using AutoMapper;
using Domains.Core;
using Domains.GoGo.Entities.Fleet;
using Domains.GoGo.Repositories.Transportation;
using Groove.AspNetCore.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domains.GoGo.Services.Transportation
{
    class VehicleFeatureRequestService : IVehicleFeatureRequestService
    {
        private readonly IVehicleFeatureRequestRepository _repository;
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public VehicleFeatureRequestService(IMapper mapper, IUnitOfWork uow, IVehicleFeatureRequestRepository repository)
        {
            _uow = uow;
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<int> CreateVehicleFeatureRequest(int requestId, int vehicleFeatureId)
        {
            var featureEnity = new VehicleFeatureRequest()
            {
                RequestId = requestId,
                VehicleFeatureId = vehicleFeatureId
            };

            _repository.Create(featureEnity);

            await _uow.SaveChangesAsync();

            return featureEnity.Id;
        }

        public DataSourceValue<int> FindVehicleFeature(int requestId)
        {
            var entity = _repository.FindVehicleFeatureAsync(requestId);
            return new DataSourceValue<int>()
            {
                Value = entity.VehicleFeature.Id,
                DisplayName = entity.VehicleFeature.FeatureName
            };
        }

        public async Task<int> UpdateVehicleFeatureAsync(int requestId, int vehicleFeatureId)
        {
            var entity = await this._repository.GetByRequestIdAsync(requestId);
            entity.VehicleFeatureId = vehicleFeatureId;
            _repository.Update(entity);
            await _uow.SaveChangesAsync();
            return entity.Id;
        }
    }

}
