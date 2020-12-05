using DataLayer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BusinessLayer;

namespace PhotoLoader
{
    class Program
    {
        static List<FileType> FileTypes;
        static int order;
        static Photos photos;
        static void Main(string[] args)
        {
            var container = new DbConnector();
            photos = new Photos(container.Container);
            FileTypes = container.Container.GetAll<FileType>().ToList();
            int count;
            try
            {
                count = RecursiveLoader(container.Container, new DirectoryInfo(@"D:\Photos"));
                container.Container.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
            Console.WriteLine(count);
        }

        private static DataLayer.File GetFile(FileInfo f, int folderId, int typeId)
        {
            return new DataLayer.File()
            {
                FolderId = folderId,
                Name = f.Name,
                Created = f.CreationTimeUtc,
                FileTypeId = typeId,
                Fullpath = f.FullName,
                SizeKB = (int?)(f.Length / 1024)
            };
        }


        private static int RecursiveLoader(DbContainer container, DirectoryInfo origin, int? parentId = null)
        {
            int count = 0;
            var dirs = origin.EnumerateDirectories();

            var originFolder = new Folder() { Name = origin.Name, ParentId = parentId };
            container.Add(originFolder);
            container.SaveChanges();
            foreach (var item in dirs)
            {
                Folder folder = new Folder() { Name = item.Name, ParentId = originFolder.Id };
                try
                {
                    container.Add(folder);
                    container.SaveChanges();

                    count++;
                    foreach (var folderFile in item.EnumerateFiles())
                    {
                        var typeId = FileTypes.Find(type => type.Name == folderFile.Extension.Substring(1).ToUpper());
                        if(typeId != null)
                        {
                            var file = GetFile(folderFile, folder.Id, typeId.Id);
                            container.Add(file);
                            container.SaveChanges();
                            var photo = photos.Get(file.Fullpath);
                        }
                    }
                    foreach (var subFolder in item.EnumerateDirectories())
                    {
                        count += RecursiveLoader(container, subFolder, folder.Id);
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine($"Failed to add folder [{folder.Name}], or File, or Photo, or Camera");
                    throw;
                }
            }
            return count;
        }
    }
}
