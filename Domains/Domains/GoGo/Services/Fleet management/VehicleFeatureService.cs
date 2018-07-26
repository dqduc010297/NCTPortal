using AutoMapper;
using Domains.Core;
using Domains.GoGo.Repositories.Fleet_management;
using Groove.AspNetCore.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domains.GoGo.Services.Fleet_management
{
    public class VehicleFeatureService : IVehicleFeatureService
    {
        private readonly IVehicleFeatureRepository _repository;
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public VehicleFeatureService(IMapper mapper, IUnitOfWork uow, IVehicleFeatureRepository repository)
        {
            _uow = uow;
            _repository = repository;
            _mapper = mapper;
        }

        public Task<IEnumerable<DataSourceValue<int>>> GetOnFilter(string displayName)
        {
            return _repository.GetOnFilter(displayName);
        }
    }
}
