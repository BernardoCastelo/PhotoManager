using Common;
using DataLayer.Exceptions;
using MetadataExtractor.Formats.Jpeg;
using MetadataExtractor.Formats.Png;
using MetadataExtractor.Formats.Tiff;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;

namespace DataLayer
{
    public static class Helper
    {
        public static DbSet<T> GetDbSet<T>(object obj) where T : class
        {
            return (DbSet<T>)Extentions.GetProperty(obj, $"{typeof(T).Name}Set");
        }

        public static FileTypeEnum GetType(string filepath)
        {
            var extension = new FileInfo(filepath).Extension.Substring(1).ToUpper();
            if (!Enum.TryParse(typeof(FileTypeEnum), new FileInfo(filepath).Extension.Substring(1).ToUpper(), out object oType))
            {
                throw new InvalidTypeException(extension);
            }
            return (FileTypeEnum)oType;
        }

        public static IEnumerable<MetadataExtractor.Directory> GetFileMetaData(string fullpath)
        {
            var type = GetType(fullpath);
            IEnumerable<MetadataExtractor.Directory> data;
            switch (type)
            {
                case FileTypeEnum.JPG:
                    data = JpegMetadataReader.ReadMetadata(fullpath);
                    break;
                case FileTypeEnum.CR2:
                    data = TiffMetadataReader.ReadMetadata(fullpath);
                    break;
                case FileTypeEnum.PNG:
                    data = PngMetadataReader.ReadMetadata(fullpath);
                    break;
                default:
                    throw new Exception(); // is unreachable
            }
            return data.ToList();
        }
    }
}
