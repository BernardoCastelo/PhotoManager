using BusinessLayer.Models;
using DataLayer;
using MetadataExtractor;
using MetadataExtractor.Formats.Jpeg;
using MetadataExtractor.Formats.Png;
using MetadataExtractor.Formats.Tiff;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BusinessLayer
{
    public class Photos
    {
        private DbContainer dbContainer;

        public Photos(DbContainer dbContainer)
        {
            this.dbContainer = dbContainer ?? throw new ArgumentNullException(nameof(dbContainer));
        }

        public Photo Get(string filepath)
        {
            var type = Helper.GetType(filepath);
            IEnumerable<MetadataExtractor.Directory> data;
            switch (type)
            {
                case FileTypeEnum.JPG:
                    data = JpegMetadataReader.ReadMetadata(filepath);
                    break;
                case FileTypeEnum.CR2:
                    data = TiffMetadataReader.ReadMetadata(filepath);
                    break;
                case FileTypeEnum.PNG:
                    data = PngMetadataReader.ReadMetadata(filepath);
                    break;
                default:
                    throw new Exception(); // is unreachable
            }

            var metaF = data.ToList();
            var att = metaF.ToList();
            var photo = new Photo()
            {
                // CameraId = cameraId,
                CategoryId = null,
                // Name = file.Name,
                Order = 0,
                //FileId = fileId,
            };

            return photo;
        }
        public Photo Get(int id)
        {
            return dbContainer.Select<Photo>(id);
        }
    }
}
