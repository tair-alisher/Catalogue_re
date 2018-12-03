using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using CatalogEntities;
using Catalogue_re.BLL.DTO;
using Catalogue_re.BLL.Exceptions;
using Catalogue_re.BLL.Interfaces;
using Catalogue_re.DAL.Interfaces;

namespace Catalogue_re.BLL.Services
{
    public class AdministrationService : IAdministrationService
    {
        private IUnitOfWork _unitOfWork { get; set; }

        public AdministrationService(IUnitOfWork uow)
        {
            _unitOfWork = uow;
        }

        public IEnumerable<AdministrationDTO> GetAll()
        {
            var administrations = _unitOfWork.Administrations.GetAll().ToList();

            return Mapper.Map<IEnumerable<AdministrationDTO>>(administrations);
        }

        public IEnumerable<AdministrationDTO> GetAllOrderedByName()
        {
            var administrations = _unitOfWork.Administrations.GetAll().OrderBy(a => a.Name).ToList();

            return Mapper.Map<IEnumerable<AdministrationDTO>>(administrations);
        }

        public IEnumerable<AdministrationDTO> GetAllOrderedByNameWithRelations()
        {
            var administrations = _unitOfWork.Administrations.GetAll().OrderBy(a => a.Name).Include(a => a.Division).ToList();

            return Mapper.Map<IEnumerable<AdministrationDTO>>(administrations);
        }

        public AdministrationDTO Get(int? id)
        {
            if (id == null)
                throw new ArgumentNullException();
            
            var administration = _unitOfWork.Administrations.GetAll().Where(a => a.Id == id).Include(a => a.Division).FirstOrDefault();
            if (administration == null)
                throw new NotFoundException();

            return Mapper.Map<AdministrationDTO>(administration);
        }

        public void Add(AdministrationDTO item)
        {
            var administration = Mapper.Map<Administration>(item);
            _unitOfWork.Administrations.Create(administration);
            _unitOfWork.Save();
        }

        public void Update(AdministrationDTO item)
        {
            var administration = Mapper.Map<Administration>(item);
            _unitOfWork.Administrations.Update(administration);
            _unitOfWork.Save();
        }

        public void Delete(int? id)
        {
            if (id == null)
                throw new ArgumentNullException();

            if (HasRelations((int)id))
                throw new HasRelationsException();

            var administration = _unitOfWork.Administrations.Get(id);
            if (administration == null)
                throw new NotFoundException();

            _unitOfWork.Administrations.Delete((int)id);
            _unitOfWork.Save();
        }

        private bool HasRelations(int id)
        {
            var relationsCount = _unitOfWork.Departments.Find(d => d.AdministrationId == id).Count();

            return relationsCount > 0;
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
    }
}
