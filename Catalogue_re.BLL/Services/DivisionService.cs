using System;
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
    public class DivisionService : IDivisionService
    {
        private IUnitOfWork _unitOfWork { get; set; }

        public DivisionService(IUnitOfWork uow)
        {
            _unitOfWork = uow;
        }

        public IEnumerable<DivisionDTO> GetAll()
        {
            var divisions = _unitOfWork.Divisions.GetAll().ToList();

            return Mapper.Map<IEnumerable<DivisionDTO>>(divisions);
        }

        public IEnumerable<DivisionDTO> GetAllOrderedByName()
        {
            var divisions = _unitOfWork.Divisions.GetAll().OrderBy(d => d.Name).ToList();

            return Mapper.Map<IEnumerable<DivisionDTO>>(divisions);
        }

        public DivisionDTO Get(int? id)
        {
            if (id == null)
                throw new ArgumentNullException();

            var division = _unitOfWork.Divisions.Get(id);
            if (division == null)
                throw new NotFoundException();

            return Mapper.Map<DivisionDTO>(division);
        }

        public void Add(DivisionDTO item)
        {
            var division = Mapper.Map<Division>(item);
            _unitOfWork.Divisions.Create(division);
            _unitOfWork.Save();
        }

        public void Update(DivisionDTO item)
        {
            var division = Mapper.Map<Division>(item);
            _unitOfWork.Divisions.Update(division);
            _unitOfWork.Save();
        }

        public void Delete(int? id)
        {
            if (id == null)
                throw new ArgumentNullException();

            if (HasRelations((int)id))
                throw new HasRelationsException();

            var division = _unitOfWork.Divisions.Get(id);
            if (division == null)
                throw new NotFoundException();

            _unitOfWork.Divisions.Delete((int)id);
            _unitOfWork.Save();
        }

        private bool HasRelations(int id)
        {
            var relationsCount = _unitOfWork.Administrations.Find(a => a.DivisionId == id).Count();

            return relationsCount > 0;
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
    }
}
