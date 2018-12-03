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
    public class PositionService : IPositionService
    {
        private IUnitOfWork _unitOfWork { get; set; }

        public PositionService(IUnitOfWork uow)
        {
            _unitOfWork = uow;
        }

        public IEnumerable<PositionDTO> GetAll()
        {
            var positions = _unitOfWork.Positions.GetAll().ToList();

            return Mapper.Map<IEnumerable<PositionDTO>>(positions);
        }

        public PositionDTO Get(int? id)
        {
            if (id == null)
                throw new ArgumentNullException();

            var position = _unitOfWork.Positions.Get(id);
            if (position == null)
                throw new NotFoundException();

            return Mapper.Map<PositionDTO>(position);
        }

        public void Add(PositionDTO item)
        {
            var position = Mapper.Map<Position>(item);
            _unitOfWork.Positions.Create(position);
            _unitOfWork.Save();
        }

        public void Update(PositionDTO item)
        {
            var position = Mapper.Map<Position>(item);
            _unitOfWork.Positions.Update(position);
            _unitOfWork.Save();
        }

        public void Delete(int? id)
        {
            if (id == null)
                throw new ArgumentNullException();

            if (HasRelations((int)id))
                throw new HasRelationsException();

            var position = _unitOfWork.Positions.Get(id);
            if (position == null)
                throw new NotFoundException();

            _unitOfWork.Positions.Delete((int)id);
            _unitOfWork.Save();
        }

        private bool HasRelations(int id)
        {
            var relationsCount = _unitOfWork.Employees.Find(e => e.PositionId == id).Count();

            return relationsCount > 0;
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
    }
}
