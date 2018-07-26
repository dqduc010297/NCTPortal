using AutoMapper;
using Domains.Core;
using Domains.GoGo.Models.Fleet_management;
using Domains.GoGo.Repositories.Fleet_management;
using Domains.GoGo.Repositories.Transportation;
using Domains.GoGo.Services.Transportation;
using Domains.Identity.Entities;
using Domains.Identity.Repositories;
using Groove.AspNetCore.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domains.GoGo.Services.Fleet_management
{
    public class DriverService : IDriverService
    {
        private readonly IDriverRepository _driverRepository;        
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public DriverService(IMapper mapper, IUnitOfWork uow, IDriverRepository repository)
        {
            _uow = uow;
            _driverRepository = repository;
            _mapper = mapper;
        } 

        public async Task<IEnumerable<DataSourceValue<long>>> GetDataSource(string driverName)
        {
            return await _driverRepository.GetDataSource(driverName);
        }

        public async Task<DriverModel> GetDriverDetail(string id)
        {
            return await _driverRepository.GetDriverDetail(id);
        }
    }
}
