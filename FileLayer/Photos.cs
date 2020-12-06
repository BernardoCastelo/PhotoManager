using DataLayer;
using System;
using System.Linq;
using System.Collections.Generic;
using Common;

namespace BusinessLayer
{
    public class Photos
    {
        private IPhotoRepository photoRepository;
        private ICameraRepository cameraRepository;
        private IFileRepository fileRepository;
        public Photos(IPhotoRepository photoRepository, ICameraRepository cameraRepository, IFileRepository fileRepository)
        {
            this.photoRepository = photoRepository ?? throw new ArgumentNullException(nameof(photoRepository));
            this.cameraRepository = cameraRepository ?? throw new ArgumentNullException(nameof(cameraRepository));
            this.fileRepository = fileRepository ?? throw new ArgumentNullException(nameof(fileRepository));
        }

        public Photo Load(string filepath)
        {
            try
            {
                var metaF = Helper.GetFileMetaData(filepath);

                var JPEGInfo = metaF.ElementAt(0).Tags;
                var ExifIFD0 = metaF.ElementAt(1).Tags;
                var ExifSubIFD = metaF.ElementAt(2).Tags;

                var imageH = JPEGInfo.FirstOrDefault(t => t.Name == "Image Height")?.Description?.RemoveUntilSpace();
                var imageW = JPEGInfo.FirstOrDefault(t => t.Name == "Image Width")?.Description?.RemoveUntilSpace();

                var cameraMaker = ExifIFD0.FirstOrDefault(t => t.Name == "Make")?.Description;
                var cameraModel = ExifIFD0.FirstOrDefault(t => t.Name == "Model")?.Description;
                if (!string.IsNullOrEmpty(cameraModel) && cameraModel.StartsWith(cameraMaker))
                {
                    cameraModel = cameraModel.Substring(cameraModel.IndexOf(" ") + 1);
                }

                var fStop = ExifSubIFD.FirstOrDefault(t => t.Name == "F-Number")?.Description;
                var ISO = ExifSubIFD.FirstOrDefault(t => t.Name == "ISO Speed Ratings")?.Description;
                var exposure = ExifSubIFD.FirstOrDefault(t => t.Name == "Exposure Time")?.Description;
                var dateTaken = ExifSubIFD.FirstOrDefault(t => t.Name == "Date/Time Original")?.Description; //2020:08:30 09:53:10
                var focalLength = ExifSubIFD.FirstOrDefault(t => t.Name == "Focal Length")?.Description?.RemoveUntilSpace(); 


                var camera = cameraRepository.Get(cameraMaker, cameraModel).FirstOrDefault();
                if(camera == null)
                {
                    camera = new Camera()
                    {
                        Maker = cameraMaker,
                        Model = cameraModel
                    };
                    cameraRepository.Insert(camera);
                }

                var file = fileRepository.Get(filepath); // File add is too complex to be included here, it is assumed the file already exists;

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
                    Height = string.IsNullOrWhiteSpace(imageH)? null : int.Parse(imageH),
                    Width = string.IsNullOrWhiteSpace(imageW) ? null : int.Parse(imageW),
                    Iso = string.IsNullOrWhiteSpace(ISO) ? null : int.Parse(ISO),
                    FocalLength = string.IsNullOrWhiteSpace(focalLength) ? null : int.Parse(focalLength)
                };
                return photo;
            }
            catch (Exception)
            {

                throw;
            }
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

        public IEnumerable<Photo> Get(int skip, int take)
        {
            try
            {
                return photoRepository.Select(skip, take);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
