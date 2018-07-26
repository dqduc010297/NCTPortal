using AutoMapper;
using Domains.Core;
using Domains.GoGo.Models.Fleet_management;
using Domains.GoGo.Repositories.Fleet_management;
using Domains.Identity.Entities;
using Domains.Identity.Repositories;
using Groove.AspNetCore.UnitOfWork.EntityFramework;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Groove.AspNetCore.DataBinding.AutoMapperExtentions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domains.GoGo.Entities;

namespace Infrastructures.Repositories.GoGo.Fleet_management
{
    public class DriverRepository : GenericRepository<User, long>, IDriverRepository
    {
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly ApplicationDbContext _dbContext;
        public DriverRepository(UserManager<User> userManager, ApplicationDbContext dbContext, IMapper mapper)
            : base(dbContext)
        {
            _userManager = userManager;
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<DriverModel> GetDriverDetail(string id)
        {
             var driverInDb = await _userManager.FindByIdAsync(id);

            _mapper.Map<DriverModel>(driverInDb);

            return _mapper.Map<DriverModel>(driverInDb);
        }

        public async Task<IEnumerable<DataSourceValue<long>>> GetDataSource(string value)
        {
			var driverIdList = this.context.Set<Shipment>().Where(p => (p.Status != ShipmentRequestStatus.COMPLETED)).Select(p => p.DriverId).ToList();

			var query = (from uRole in _dbContext.UserRoles
						 from user in dbSet
						 from role in _dbContext.Roles
						 where uRole.UserId == user.Id && role.Id == uRole.RoleId
						 && ((user.UserName.Contains(value) || user.PhoneNumber.Contains(value) || user.Id.ToString().Contains(value) || user.Id.ToString() == value) && !driverIdList.Contains(user.Id))
						 && role.Name == "Driver"
						 select new DataSourceValue<long>
						 {
							 DisplayName = user.UserName,
							 Value = user.Id
						 });

			return await query.ToListAsync();
		}
    }
}
