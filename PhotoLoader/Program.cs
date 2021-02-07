using BusinessLayer;
using Common;
using DataLayer;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoLoader
{
    class Program
    {
        static List<FileType> FileTypes;
        static PhotoRepository photoRep;
        static Photos photos;
        static async Task Main()
        {
            var container = new DbConnector();
            photoRep = new PhotoRepository(container.Container);
            photos = new Photos(photoRep, new CameraRepository(container.Container), new FileRepository(container.Container));

            FileTypes = container.Container.SelectAll<FileType>().ToList();
            try
            {

                UpdateExposureAndFNumberValues(container.Container);

                //long timeTaken = 0;
                //timeTaken = LoadFolders(container.Container, new DirectoryInfo(Constants.Folders.Main));
                //Console.WriteLine($"{nameof(LoadFolders)}: {timeTaken}ms");

                //timeTaken = LoadFiles(container.Container, new DirectoryInfo(Constants.Folders.Main), true);
                //Console.WriteLine($"{nameof(LoadFiles)}: {timeTaken}ms");

                //var jpg = container.Container.FileTypeSet.First(f => f.Name == FileTypeEnum.JPG.ToString());
                //timeTaken = await CreateThumbnails(container.Container, Constants.Folders.Thumbnails, jpg);
                //Console.WriteLine($"{nameof(CreateThumbnails)}: {timeTaken}ms");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        private static DataLayer.File GetFile(FileInfo fileInfo, int folderId, int typeId)
        {
            return new DataLayer.File()
            {
                FolderId = folderId,
                Name = fileInfo.Name,
                Created = fileInfo.CreationTimeUtc,
                FileTypeId = typeId,
                Fullpath = fileInfo.FullName,
                SizeKB = (int?)(fileInfo.Length / 1024)
            };
        }

        private static long LoadFiles(DbContainer container, DirectoryInfo origin, bool update, int? parentId = null)
        {
            Console.WriteLine($"Loading Files on folder [{origin.FullName}].");
            var sw = new Stopwatch();
            sw.Start();

            var originFolder = container.FolderSet.FirstOrDefault(f => f.Name == origin.Name && f.ParentId == parentId);
            if (originFolder != null)
            {
                var added = 0;
                var updated = 0;
                foreach (var folderFile in origin.EnumerateFiles())
                {
                    Console.WriteLine($"[ONGOING] {folderFile.FullName} executionTime: {sw.ElapsedMilliseconds} ");

                    var type = FileTypes.Find(type => type.Name == folderFile.Extension[1..].ToUpper());
                    if (type == null)
                    {
                        continue;
                    }

                    var file = container.FileSet.FirstOrDefault(f => f.Fullpath == folderFile.FullName);
                    if (file == null)
                    {
                        file = GetFile(folderFile, originFolder.Id, type.Id);
                        container.Add(file);
                    }

                    var photo = container.PhotoSet.FirstOrDefault(f => f.FileId == file.Id);
                    if (photo == null && !update)
                    {
                        added++;
                        photo = photos.Load(file.Fullpath);
                        if (photo != null)
                        {
                            if (type.Name != FileTypeEnum.CR2.ToString())
                            {
                                var image = Image.FromFile(file.Fullpath);

                                var thumb = image.Width > 0 && image.Height > 0 ?
                                    image.GetThumbnailImage(image.Width / Constants.ImageProperties.ThumbMultiplier, image.Height / Constants.ImageProperties.ThumbMultiplier, () => false, IntPtr.Zero) :
                                    image.GetThumbnailImage(Constants.ImageProperties.ThumbWidth, Constants.ImageProperties.ThumbHeight, () => false, IntPtr.Zero);
                                
                                photo.Thumbnail = new ImageConverter().ConvertTo(thumb, typeof(byte[])) as byte[];
                                image.Dispose();
                                thumb.Dispose();
                            }
                            photoRep.Insert(photo);
                        }
                    }
                    else
                    {
                        var newPhoto = photos.Load(file.Fullpath);
                        photo.Iso = newPhoto.Iso;
                        photo.Height = newPhoto.Height;
                        photo.Width = newPhoto.Width;
                        photo.FocalLength = newPhoto.FocalLength;
                        photo.FStop = newPhoto.FStop;
                        photo.DateTaken = newPhoto.DateTaken;

                        photoRep.Update(photo);
                        updated++;
                    }
                }
                container.SaveChanges();
                Console.WriteLine($"Added: {added}; updated/skipped: {updated}; executionTime: {sw.ElapsedMilliseconds} ");

                var dirs = origin.EnumerateDirectories();
                foreach (var folder in dirs)
                {
                    LoadFiles(container, folder, true, originFolder.Id);
                }
            }

            sw.Stop();
            return sw.ElapsedMilliseconds;
        }

        private static long LoadFolders(DbContainer container, DirectoryInfo origin, int? parentId = null)
        {
            var sw = new Stopwatch();
            sw.Start();

            var originFolder = container.FolderSet.FirstOrDefault(f => f.Name == origin.Name && f.ParentId == parentId);
            if (originFolder == null)
            {
                originFolder = new Folder() { Name = origin.Name, ParentId = parentId };
                container.Add(originFolder);
                container.SaveChanges();
            }

            var dirs = origin.EnumerateDirectories();

            foreach (var folder in dirs)
            {
                LoadFolders(container, folder, originFolder.Id);
            }

            sw.Stop();
            return sw.ElapsedMilliseconds;
        }

        private static async Task<long> CreateThumbnails(DbContainer container, string outputFolder, FileType typeFilter)
        {
            var sw = new Stopwatch();
            sw.Start();

            int numThreads = 4;

            var photos = photoRep.SelectAll();
            var count = photos.Count();

            var partial = count / numThreads;

            List<Task<long>> tasks = new List<Task<long>>();

            for (int i = 0; i < numThreads; i++)
            {
                var skip = i * partial;
                var partialPhotos = photoRep.Select(skip, partial).ToList();
                var files = container.FileSet.Select(a => a).ToList();

                var task = new Task<long>(() => ThumbTask(partialPhotos, files, outputFolder, typeFilter, i));
                task.Start();
                tasks.Add(task);
            }

            var mainTask = Task.WhenAll(tasks);

            var results = await mainTask;

            for (int i = 0; i < numThreads; i++)
            {
                Console.WriteLine($"Thread Nº{results} took {results[i]}ms.");
            }
            sw.Stop();
            return sw.ElapsedMilliseconds;
        }

        private static long ThumbTask(List<Photo> photos, List<DataLayer.File> files, string outputFolder, FileType typeFilter, int taskNumber)
        {
            Console.WriteLine($"Loading Thumbnails");
            var sw = new Stopwatch();
            sw.Start();

            foreach (var photo in photos)
            {
                var file = files.First(f => f.Id == photo.FileId);
                var newFileName = $"{outputFolder}\\{Path.ChangeExtension(photo.Name, "thumb")}";

                if (file == null || file.FileTypeId != typeFilter.Id || System.IO.File.Exists(newFileName))
                {
                    continue;
                }

                Console.WriteLine($"Thread #{taskNumber}: {file.Fullpath}");
                var image = Image.FromFile(file.Fullpath);

                var thumb = image.GetThumbnailImage(Constants.ImageProperties.ThumbWidth, Constants.ImageProperties.ThumbHeight, () => false, IntPtr.Zero);

                try
                {
                    thumb.Save(newFileName);
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Thread #{taskNumber} ERROR: {e.Message}");
                }
                finally
                {
                    image.Dispose();
                    thumb.Dispose();
                }
            }

            sw.Stop();
            return sw.ElapsedMilliseconds;
        }

        private static void UpdateExposureAndFNumberValues(DbContainer container)
        {
            var photos = container.SelectAll<Photo>();
            foreach (var photo in photos)
            {
                var exposureString = photo.Exposure;
                if (!string.IsNullOrEmpty(exposureString))
                {
                    photo.ExposureAsNumber = GetFromFraction(exposureString.Substring(0, exposureString.IndexOf('s')));
                }

                var fStopString = photo.FStop;
                if (!string.IsNullOrEmpty(fStopString))
                {
                    photo.FStopAsNumber = double.Parse(fStopString[2..]);
                }

                var entity = container.Attach(photo);
                entity.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                container.PhotoSet.Update(photo);
                container.SaveChanges();
            }
        }

        private static double GetFromFraction(string fraction)
        {
            // 312312/327819312
            int slashIndex = fraction.IndexOf('/');
            if(slashIndex <= 0)
            {
                return double.Parse(fraction);
            }
            double left = double.Parse(fraction.Substring(0, slashIndex));
            double right = double.Parse(fraction[(slashIndex + 1)..]);

            return left / right;
        }
    }
}
