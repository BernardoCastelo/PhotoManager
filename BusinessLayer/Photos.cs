using Common;
using DataLayer;
using DataLayer.Dtos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BusinessLayer
{
    public class Photos : IPhotos
    {
        private readonly ICameraRepository cameraRepository;
        private readonly ICategoryRepository categoryRepository;
        private readonly IFileRepository fileRepository;
        private readonly IPhotoRepository photoRepository;

        public Photos(IPhotoRepository photoRepository, ICameraRepository cameraRepository, IFileRepository fileRepository, ICategoryRepository categoryRepository)
        {
            this.cameraRepository = cameraRepository ?? throw new ArgumentNullException(nameof(cameraRepository));
            this.categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
            this.fileRepository = fileRepository ?? throw new ArgumentNullException(nameof(fileRepository));
            this.photoRepository = photoRepository ?? throw new ArgumentNullException(nameof(photoRepository));
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
                cameraRepository.Add(camera);
            }

            var file = fileRepository.Select(filepath);

            double? fLength = focalLength != null ? double.Parse(focalLength) : null;
            fLength = fLength == 0 ? null : fLength;

            var photo = new Photo()
            {
                CameraId = string.IsNullOrEmpty(cameraMaker) && string.IsNullOrEmpty(cameraModel) ? null : camera.Id,
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
                return photoRepository.SelectThumbnails(skip, take, GetOrderBy(orderBy), orderByDescending);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<Photo> Get(int skip, int take, string orderBy = null, bool orderByDescending = false, List<Filter> filters = null)
        {
            try
            {
                if (filters?.Any() ?? false)
                {
                    filters.Add(new Filter
                    {
                        Negate = true,
                        FieldName = nameof(Photo.Thumbnail),
                        Value = null
                    });

                    var folderNameFilter = filters.FirstOrDefault(filter => filter.FieldName == Constants.Filters.folderName);

                    if (folderNameFilter != null)
                    {
                        filters.Remove(folderNameFilter);
                    }

                    filters.ForEach(filter =>
                    {
                        if (filter.FieldName.ToLower() == nameof(Photo.Exposure).ToLower())
                        {
                            filter.FieldName = nameof(Photo.ExposureAsNumber);
                        }
                        if (filter.FieldName.ToLower() == nameof(Photo.FStop).ToLower())
                        {
                            filter.FieldName = nameof(Photo.FStopAsNumber);
                        }
                    });

                    if (folderNameFilter != null)
                    {
                        var validFileIds = fileRepository.SelectFullpathContainsText(folderNameFilter.Value as string).Select(file => file.Id);
                        return photoRepository
                            .Select(skip, take, GetOrderBy(orderBy), orderByDescending, filters)
                            .Join(validFileIds,
                                photo => photo.FileId,
                                fileId => fileId,
                                (photo, fileId) => photo)
                            .Skip(skip)
                            .Take(take)
                            .ToList();
                    }
                    else
                    {
                        return photoRepository.Select(skip, take, GetOrderBy(orderBy), orderByDescending, filters)
                            .Skip(skip)
                            .Take(take)
                            .ToList();
                    }
                }

                return photoRepository.SelectThumbnails(skip, take, GetOrderBy(orderBy), orderByDescending);
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

        public IEnumerable<Category> GetCategories(int id)
        {
            try
            {
                return categoryRepository.GetByPhotoId(id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private static string GetOrderBy(string orderBy)
        {
            if (!string.IsNullOrEmpty(orderBy))
            {
                var lowered = orderBy.ToLower();
                if (lowered == nameof(Photo.Exposure).ToLower())
                {
                    return nameof(Photo.ExposureAsNumber);
                }
                else if (lowered == nameof(Photo.FStop).ToLower())
                {
                    return nameof(Photo.FStopAsNumber);
                }
            }
            return orderBy;
        }
    }
}
