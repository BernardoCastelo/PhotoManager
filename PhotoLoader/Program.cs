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
        static CameraRepository cameraRep;
        static async Task Main()
        {
            var container = new DbConnector();
            photoRep = new PhotoRepository(container.Container);
            cameraRep = new CameraRepository(container.Container);
            photos = new Photos(photoRep, cameraRep, new FileRepository(container.Container));

            FileTypes = container.Container.SelectAll<FileType>().ToList();
            try
            {
                long timeTaken = 0;
                timeTaken = LoadFolders(container.Container, new DirectoryInfo(@"D:\Photos"));
                Console.WriteLine($"LoadFolders: {timeTaken}ms");

                timeTaken = LoadFiles(container.Container, new DirectoryInfo(@"D:\Photos"));
                Console.WriteLine($"LoadFiles: {timeTaken}ms");

                //var jpg = container.Container.FileTypeSet.First(f => f.Name == "JPG");
                //timeTaken = await CreateThumbnails(container.Container, @"C:\PhotoThumbnails", jpg);
                //Console.WriteLine($"CreateThumbnails: {timeTaken}ms");
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

        private static long LoadFiles(DbContainer container, DirectoryInfo origin, int? parentId = null)
        {
            Console.WriteLine($"Loading Files on folder [{origin.FullName}].");
            var sw = new Stopwatch();
            sw.Start();

            var originFolder = container.FolderSet.FirstOrDefault(f => f.Name == origin.Name && f.ParentId == parentId);
            if (originFolder != null)
            {
                var added = 0;
                var skipped = 0;
                foreach (var folderFile in origin.EnumerateFiles())
                {
                    Console.WriteLine($"[ONGOING] Added: {folderFile.FullName} executionTime: {sw.ElapsedMilliseconds} ");

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
                    if (photo == null)
                    {
                        added++;
                        photo = photos.Load(file.Fullpath);
                        if (photo != null)
                        {
                            if (type.Name != FileTypeEnum.CR2.ToString())
                            {
                                var image = Image.FromFile(file.Fullpath);
                                var thumb = image.GetThumbnailImage(Constants.ImageProperties.ThumbWidth, Constants.ImageProperties.ThumbHeight, () => false, IntPtr.Zero);
                                photo.Thumbnail = new ImageConverter().ConvertTo(thumb, typeof(byte[])) as byte[];
                                image.Dispose();
                                thumb.Dispose();
                            }
                            photoRep.Insert(photo);
                        }
                    }
                    else
                    {
                        skipped++;
                    }
                }
                container.SaveChanges();
                Console.WriteLine($"[] Added: {added}; Skipped: {skipped}; executionTime: {sw.ElapsedMilliseconds} ");

                var dirs = origin.EnumerateDirectories();
                foreach (var folder in dirs)
                {
                    LoadFiles(container, folder, originFolder.Id);
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
    }
}
