using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domains.Core;
using Domains.GoGo.Entities;
using Domains.GoGo.Models.Fleet_management;
using Domains.GoGo.Repositories.Fleet_management;
using Groove.AspNetCore.UnitOfWork;

namespace Domains.GoGo.Services.Fleet_management
{
    public class VehicleService : IVehicleService
    {
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public VehicleService(IMapper mapper, IUnitOfWork uow, IVehicleRepository repository)
        {
            _uow = uow;
            _vehicleRepository = repository;
            _mapper = mapper;
        }

         public async Task<VehicleModel> GetVehicleDetailAsync(int id)
        {
            return await _vehicleRepository.GetVehicleDetailAsync(id);
        }

        public async Task<IEnumerable<DataSourceValue<int>>> GetDataSource(string licensePlate)
        {
            return await _vehicleRepository.GetDataSource(licensePlate);
        }
    }
}
