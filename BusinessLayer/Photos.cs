using Common;
using DataLayer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BusinessLayer
{
    public class Photos : IPhotos
    {
        private readonly IPhotoRepository photoRepository;
        private readonly ICameraRepository cameraRepository;
        private readonly IFileRepository fileRepository;
        public Photos(IPhotoRepository photoRepository, ICameraRepository cameraRepository, IFileRepository fileRepository)
        {
            this.photoRepository = photoRepository ?? throw new ArgumentNullException(nameof(photoRepository));
            this.cameraRepository = cameraRepository ?? throw new ArgumentNullException(nameof(cameraRepository));
            this.fileRepository = fileRepository ?? throw new ArgumentNullException(nameof(fileRepository));
        }

        public Photo Load(string filepath)
        {
            var metaF = Helper.GetFileMetaData(filepath);

            var JPEGInfo = metaF.ElementAt(0).Tags;
            var ExifIFD0 = metaF.ElementAt(1).Tags;
            var ExifSubIFD = metaF.ElementAt(2).Tags;

            var imageH = JPEGInfo.FirstOrDefault(t => t.Name == Constants.ImageProperties.ImageHeight)?.Description?.RemoveUntilSpace();
            var imageW = JPEGInfo.FirstOrDefault(t => t.Name == Constants.ImageProperties.ImageHWidth)?.Description?.RemoveUntilSpace();

            var cameraMaker = ExifIFD0.FirstOrDefault(t => t.Name == Constants.ImageProperties.CameraMake)?.Description;
            var cameraModel = ExifIFD0.FirstOrDefault(t => t.Name == Constants.ImageProperties.CameraModel)?.Description;
            if (!string.IsNullOrEmpty(cameraModel) && cameraModel.StartsWith(cameraMaker))
            {
                cameraModel = cameraModel[(cameraModel.IndexOf(" ") + 1)..];
            }

            var fStop = ExifSubIFD.FirstOrDefault(t => t.Name == Constants.ImageProperties.FNumber)?.Description;
            var ISO = ExifSubIFD.FirstOrDefault(t => t.Name == Constants.ImageProperties.ISO)?.Description;
            var exposure = ExifSubIFD.FirstOrDefault(t => t.Name == Constants.ImageProperties.Exposure)?.Description;
            var dateTaken = ExifSubIFD.FirstOrDefault(t => t.Name == Constants.ImageProperties.DateTIme)?.Description; //2020:08:30 09:53:10
            var focalLength = ExifSubIFD.FirstOrDefault(t => t.Name == Constants.ImageProperties.FocalLength)?.Description?.RemoveUntilSpace();


            var camera = cameraRepository.Get(cameraMaker, cameraModel).FirstOrDefault();
            if (camera == null)
            {
                camera = new Camera()
                {
                    Maker = cameraMaker,
                    Model = cameraModel
                };
                cameraRepository.Insert(camera);
            }

            var file = fileRepository.Select(filepath);

            double? fLength = focalLength != null ? double.Parse(focalLength) : null;
            fLength = fLength == 0 ? null : fLength;

            var photo = new Photo()
            {
                CameraId = string.IsNullOrEmpty(cameraMaker) && string.IsNullOrEmpty(cameraModel) ? null : camera.Id,
                CategoryId = null,
                Name = file.Name,
                Order = 0,
                FileId = file.Id,
                DateTaken = dateTaken.ParseIntoDateTime(),
                FStop = fStop,
                Exposure = exposure,
                Height = string.IsNullOrWhiteSpace(imageH) ? null : int.Parse(imageH),
                Width = string.IsNullOrWhiteSpace(imageW) ? null : int.Parse(imageW),
                Iso = string.IsNullOrWhiteSpace(ISO) ? null : int.Parse(ISO),
                FocalLength = fLength.HasValue ? (int)fLength : null
            };
            return photo;
        }

        public string GetBytes(int id)
        {
            var photo = Get(id);
            if (photo == null)
            {
                throw new NullReferenceException(nameof(photo));
            }

            if (!photo.FileId.HasValue)
            {
                throw new FileNotFoundException($"PhotoId:'{photo.Id}'; PhotoName:'{photo.Name}'");
            }

            var file = fileRepository.Select(photo.FileId.Value);
            if (file == null)
            {
                throw new NullReferenceException(nameof(file));
            }

            var bytes = System.IO.File.ReadAllBytes(file.Fullpath);
            return Convert.ToBase64String(bytes);
        }

        public Photo Get(int id)
        {
            try
            {
                return photoRepository.Select(id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<Photo> Get(int skip, int take, string orderBy = null, bool orderByDescending = false)
        {
            try
            {
                return photoRepository.SelectThumbnails(skip, take, orderBy, orderByDescending);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string GetFullPath(int id)
        {
            try
            {
                var photo = photoRepository.Select(id);
                if (photo.FileId is int validId)
                {
                    return fileRepository.Select(validId).Fullpath;
                }
                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
