using DataLayer;
using System;
using System.Collections.Generic;

namespace BusinessLayer
{
    public class Categories : ICategories
    {
        private readonly ICategoryRepository categoryRepository;

        public Categories(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
        }

        public Category Get(int id)
        {
            try
            {
                return categoryRepository.Select(id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<Category> Get(IEnumerable<int> ids)
        {
            try
            {
                return categoryRepository.Select(ids);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<Category> GetAll()
        {
            try
            {
                return categoryRepository.SelectAll();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
