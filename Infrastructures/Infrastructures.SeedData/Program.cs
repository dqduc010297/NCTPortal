using Domains.GoGo;
using Domains.GoGo.Entities;
using Domains.GoGo.Entities.Fleet;
using Domains.Identity.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructures.SeedData
{
    class Program
    {
        private static IServiceProvider _serviceProvider;

        static void Main(string[] args)
        {

            Console.WriteLine("Starting to seed data");

            _serviceProvider = ConfigureService(new ServiceCollection(), args);



            using (var dbContext = _serviceProvider.GetService<ApplicationDbContext>())
            {
                using (var transaction = dbContext.Database.BeginTransaction())
                {
                    SeedDataAsync(dbContext).Wait();
                    transaction.Commit();

                    Console.WriteLine("Commit all seed");
                }
            }
            Console.WriteLine("Seed data successful");
        }

        private static IServiceProvider ConfigureService(IServiceCollection services, string[] args)
        {
            var dbContextFactory = new DesignTimeDbContextFactory();

            services.AddLogging();
            services.AddScoped<ApplicationDbContext>(p => dbContextFactory.CreateDbContext(args));

            services.AddIdentity<User, Role>()
                            .AddEntityFrameworkStores<ApplicationDbContext>()
                            .AddDefaultTokenProviders(); // protection provider responsible for generating an email confirmation token or a password reset token
            services.Configure<IdentityOptions>(options =>
            {
                // Password settings
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = false;

                // User settings
                options.User.RequireUniqueEmail = true;
            });

            // Build the IoC from the service collection
            return services.BuildServiceProvider();
        }

        private static async Task SeedDataAsync(ApplicationDbContext dbContext)
        {
            //await SeedUserDataAsync(dbContext);
            //await SeedRoleDataAsync(dbContext);
            //await SeedAdministratortDataAsync(dbContext);
            //await SeedCoordinatorDataAsync(dbContext);
            //await SeedDriverDataAsync(dbContext);
            //await SeedCustomerDataAsync(dbContext);

            //SeedWarehouseData(dbContext);

            //SeedVehicleTypeDataAsync(dbContext);
            //SeedVehicleData(dbContext);
            //SeedVehicleFeatureData(dbContext);
            //SeedDriverAbilityData(dbContext);
            //SeedFeatureOfVehicleData(dbContext);

            SeedRequestData(dbContext);
            SeedShipmentData(dbContext);
            SeedShipmentRequestData(dbContext);
            SeedVehicleFeatureRequest(dbContext);
        }

        private static string ConverIntToString(int input)
        {
            string output = "";
            if (input < 10)
                output = "0" + input.ToString();
            else output = input.ToString();
            return output;
        }
        //Generate code
        private static string GenerateCode(DateTime dateTime, int id)
        {
            string day = ConverIntToString(dateTime.Day);
            string month = ConverIntToString(dateTime.Month);
            string hour = ConverIntToString(dateTime.Hour);
            string minute = ConverIntToString(dateTime.Minute);

            string code = day + month + dateTime.Year.ToString() + hour + minute + "GG" + id.ToString();
            return code;
        }

        //Add seed data of User
        private static async Task SeedUserDataAsync(ApplicationDbContext dbContext)
        {
            if (!await dbContext.Set<User>().AnyAsync())
            {
                Console.WriteLine("Start to seed user info");
                var userManagement = _serviceProvider.GetService<UserManager<User>>();
                var user = new User
                {
                    UserName = "system",
                    Email = "GoGo@groovetechnology.com",
                    PhoneNumber = "0909123007",
                    CreatedByUserId = 1,
                    CreatedDate = DateTimeOffset.UtcNow,
                    CreatedByUserName = "system",
                    UpdatedByUserId = 1,
                    UpdatedDate = DateTimeOffset.UtcNow,
                    UpdatedByUserName = "system",
			
                    Status = "Active"
                };

                await userManagement.CreateAsync(user, "Password@1");

                Console.WriteLine("Finish seed user info");
            }
        }
		private static void SeedVehicleFeatureRequest(ApplicationDbContext dbContext)
		{
			for (int i = 1; i < 25; i++)
			{
				var vehicleFeatureRequest = new VehicleFeatureRequest
				{
					RequestId = i,
					VehicleFeatureId = 1
				};

				dbContext.Add(vehicleFeatureRequest);
			}
			dbContext.SaveChanges();
		}
		private static async Task SeedCustomerDataAsync(ApplicationDbContext dbContext)
        {
            Console.WriteLine("Start to seed user info");
            string[] name = { "Jacob", "Michael", "Joshua", "Matthew", "Daniel" };
            string[] lastname = { "Smith", "Johnson", "Williams", "Brown", "Jones" };
            Random random = new Random();
            var userManagement = _serviceProvider.GetService<UserManager<User>>();
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    var user = new User
                    {
                        FirstName = name[i],
                        LastName = lastname[j],
                        UserName = name[i] + lastname[j],
                        Email = name[i] + "." + lastname[j] + (i + j).ToString() + "@groovetechnology.com",
                        PhoneNumber = "0909" + random.Next(100000,999999).ToString(),
                        Status = "Active",
                        CreatedByUserId = 1,
                        CreatedDate = DateTimeOffset.UtcNow,
                        CreatedByUserName = "system",
                        UpdatedByUserId = 1,
                        UpdatedDate = DateTimeOffset.UtcNow,
				
                        UpdatedByUserName = "system"
                    };
                    IdentityResult rs = await userManagement.CreateAsync(user, "P@ssword123");
                    if (rs.Succeeded)
                        await userManagement.AddToRoleAsync(user, "Customer");
                    else
                    {
                        Console.WriteLine(rs.Errors);
                    }
                }
            }
            Console.WriteLine("Finish seed user info");
        }
        private static async Task SeedDriverDataAsync(ApplicationDbContext dbContext)
        {
            // TODO: Refact Seed data to be English base
            Console.WriteLine("Start to seed user info");
            string[] name = { "Christopher", "Andrew", "Ethan", "Joseph", "William" };
            string[] lastname = { "Miller", "Davis", "Garcia", "Rodriguez", "Wilson" };
            Random random = new Random();
            var userManagement = _serviceProvider.GetService<UserManager<User>>();
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    var user = new User
                    {
                        FirstName = name[i],
                        LastName = lastname[j],
                        UserName = name[i] + lastname[j],
                        Email = name[i] + "." + lastname[j] + (i + j).ToString() + "@groovetechnology.com",
                        PhoneNumber = "0909" + random.Next(100000, 999999).ToString(),
                        Status = "Active",
                        CreatedByUserId = 1,
                        CreatedDate = DateTimeOffset.UtcNow,
                        CreatedByUserName = "system",
                        UpdatedByUserId = 1,
                        UpdatedDate = DateTimeOffset.UtcNow,
			
						UpdatedByUserName = "system"
                    };
                    IdentityResult rs = await userManagement.CreateAsync(user, "P@ssword123");
                    if (rs.Succeeded)
                        await userManagement.AddToRoleAsync(user, "Driver");
                    else
                    {
                        Console.WriteLine(rs.Errors);
                    }
                }
            }
            Console.WriteLine("Finish seed user info");
        }
        private static async Task SeedCoordinatorDataAsync(ApplicationDbContext dbContext)
        {
            Console.WriteLine("Start to seed user info");
            string[] name = { "Anthony", "David", "Alexander", "Nicholas", "Ryan" };
            string[] lastname = { "Martinez", "Anderson", "Taylor", "Thomas", "Hernandez" };
            Random random = new Random();
            var userManagement = _serviceProvider.GetService<UserManager<User>>();
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    var user = new User
                    {
                        FirstName = name[i],
                        LastName = lastname[j],
                        UserName = name[i] + lastname[j],
                        Email = name[i] + "." + lastname[j] + (i + j).ToString() + "@groovetechnology.com",
                        PhoneNumber = "0909" + random.Next(100000,999999).ToString(),
                        Status = "Active",
                        CreatedByUserId = 1,
                        CreatedDate = DateTimeOffset.UtcNow,
                        CreatedByUserName = "system",
                        UpdatedByUserId = 1,
				
						UpdatedDate = DateTimeOffset.UtcNow,
                        UpdatedByUserName = "system"
                    };
                    IdentityResult rs = await userManagement.CreateAsync(user, "P@ssword123");
                    if (rs.Succeeded)
                        await userManagement.AddToRoleAsync(user, "Coordinator");
                    else
                    {
                        Console.WriteLine(rs.Errors);
                    }
                }
            }
            Console.WriteLine("Finish seed user info");
        }
        private static async Task SeedAdministratortDataAsync(ApplicationDbContext dbContext)
        {
            Console.WriteLine("Start to seed user info");
            string[] name = { "Tyler", "James", "John", "Jonathan", "Noah" };
            string[] lastname = { "Moore", "Martin", "Jackson", "Thompson", "White" };
            Random random = new Random();
            var userManagement = _serviceProvider.GetService<UserManager<User>>();
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    var user = new User
                    {
                        FirstName = name[i],
                        LastName = lastname[j],
                        UserName = name[i] + lastname[j],
                        Email = name[i] + "." + lastname[j] + (i + j).ToString() + "@groovetechnology.com",
                        PhoneNumber = "0909" + random.Next(100000,999999).ToString(),
                        Status = "Active",
                        CreatedByUserId = 1,
                        CreatedDate = DateTimeOffset.UtcNow,
                        CreatedByUserName = "system",
                        UpdatedByUserId = 1,
						UpdatedDate = DateTimeOffset.UtcNow,
                        UpdatedByUserName = "system"
                    };
                    IdentityResult rs = await userManagement.CreateAsync(user, "P@ssword123");
                    if (rs.Succeeded)
                        await userManagement.AddToRoleAsync(user, "Administrator");
                    else
                    {
                        Console.WriteLine(rs.Errors);
                    }
                }
            }
            Console.WriteLine("Finish seed user info");
        }
        private static async Task SeedRoleDataAsync(ApplicationDbContext dbContext)
        {
            var roleManagement = _serviceProvider.GetService<RoleManager<Role>>();
            var roleCustomer = new Role
            {
                Name = "Customer",
                CreatedByUserId = 1,
                CreatedDate = DateTimeOffset.UtcNow,
                CreatedByUserName = "system",
                ModifiedByUserId = 1,
                ModifiedDate = DateTimeOffset.UtcNow,
                ModifiedByUserName = "system"
            };
            await roleManagement.CreateAsync(roleCustomer);
            var roleDriver = new Role
            {
                Name = "Driver",
                CreatedByUserId = 1,
                CreatedDate = DateTimeOffset.UtcNow,
                CreatedByUserName = "system",
                ModifiedByUserId = 1,
                ModifiedDate = DateTimeOffset.UtcNow,
                ModifiedByUserName = "system"
            };
            await roleManagement.CreateAsync(roleDriver);
            var roleCoordinator = new Role
            {
                Name = "Coordinator",
                CreatedByUserId = 1,
                CreatedDate = DateTimeOffset.UtcNow,
                CreatedByUserName = "system",
                ModifiedByUserId = 1,
                ModifiedDate = DateTimeOffset.UtcNow,
                ModifiedByUserName = "system"
            };
            await roleManagement.CreateAsync(roleCoordinator);
            var roleAccountManager = new Role
            {
                Name = "Administrator",
                CreatedByUserId = 1,
                CreatedDate = DateTimeOffset.UtcNow,
                CreatedByUserName = "system",
                ModifiedByUserId = 1,
                ModifiedDate = DateTimeOffset.UtcNow,
                ModifiedByUserName = "system"
            };
            await roleManagement.CreateAsync(roleAccountManager);
        }

        //Add seed data of Vehicle
        private static void SeedVehicleTypeDataAsync(ApplicationDbContext dbContext)
        {
            Console.WriteLine("Start to seed Vehicle Type info");
            string[] TypeName = { "Small", "Medium", "Large" };
            int[] Weight = { 40, 90, 147 };
            for (int i = 0; i < 3; i++)
            {
                var vehicleType = new VehicleType
                {
                    TypeName = TypeName[i],
                    Weight = Weight[i]

                };
                dbContext.Add(vehicleType);
            }
            dbContext.SaveChanges();
            Console.WriteLine("Finish seed user info");
        }
        private static void SeedVehicleData(ApplicationDbContext dbContext)
        {
            Console.WriteLine("Start to seed Vehicle info");
            float[] Height = { 2700, 2900, 2800 };
            float[] Width = { 2350, 2400, 2400 };
            float[] Lenght = { 10200, 12000, 14000 };
            string licenseplate = "29C-";
            string licenseplate_base = "00000";

            for (int i = 0; i < 200; i++)
            {
                //
                string licenseplate_current = licenseplate_base + (i + 1).ToString();
                int lastchar = licenseplate_current.Length;
                licenseplate_current = licenseplate_current.Substring(lastchar - 5, 5);
                int type = i % 3;
                var vehicle = new Vehicle
                {
                    Height = Height[type],
                    Lenght = Lenght[type],
                    Width = Width[type],
                    LicensePlate = licenseplate + licenseplate_current,
                    VehicleTypeId = type + 1
                };
                dbContext.Add(vehicle);
            }
            dbContext.SaveChanges();
            Console.WriteLine("Finish seed user info");
        }
        private static void SeedVehicleFeatureData(ApplicationDbContext dbContext)
        {

            var vehicleFeatureIB = new VehicleFeature
            {
                FeatureName = "Insulated Box"
            };
            dbContext.Add(vehicleFeatureIB);
            var vehicleFeature = new VehicleFeature
            {
                FeatureName = "Normal"
            };
            dbContext.Add(vehicleFeature);
            dbContext.SaveChanges();
        }
        private static void SeedDriverAbilityData(ApplicationDbContext dbContext)
        {
            for (int i = 0; i < 25; i++)
            {
                var driverAbility = new DriverAbility
                {
                    DriverId = (i + 52),
                    VehicleTypeId = i % 3 + 1
                };
                dbContext.Add(driverAbility);
            }
            dbContext.SaveChanges();
        }
        private static void SeedFeatureOfVehicleData(ApplicationDbContext dbContext)
        {
            for (int i = 1; i < 101; i++)
            {
                var featureOfVehicle = new FeatureOfVehicle
                {
                    VehicleId = i,
                    VehicleFeatureId = 1
                };
                dbContext.Add(featureOfVehicle);
            }
            for (int i = 101; i < 201; i++)
            {
                var featureOfVehicle = new FeatureOfVehicle
                {
                    VehicleId = i,
                    VehicleFeatureId = 2
                };
                dbContext.Add(featureOfVehicle);
            }
            dbContext.SaveChanges();
        }


        //Add seed data of Warehouse
        private static void SeedWarehouseData(ApplicationDbContext dbContext)
        {
            Random ran = new Random();
            string phonenumberHeader = "0909";
            double latitudeBase = 10.767089;
            double longitudeBase = 106.706589;
            for (double i = 0; i < 25; i++)
            {
                long custormerID = (long)i + 76;
                var warehouse = new WareHouse
                {
                    NameWarehouse = "WH" + (i + 1).ToString(),
                    PhoneNumber = phonenumberHeader + ran.Next(100000, 999999).ToString(),
                    OwnerId = custormerID,
                    Latitude = Math.Round(latitudeBase + i * 0.0001, 6),
                    Longitude = Math.Round(longitudeBase + i * 0.0001, 6),
                    Address = "53 Đoàn Như Hài, Quận 4, Hồ Chí Minh, Vietnam"
                };
                dbContext.Add(warehouse);
                dbContext.SaveChanges();
            }
        }
        private static DateTime FormatDateTime(DateTime dateTime)
        {
            String dateTimeString = String.Format("{0:g}", dateTime);
            return DateTime.Parse(dateTimeString);
        }
        //Add seed data of Request
        private static void SeedRequestData(ApplicationDbContext dbContext)
        {
            double latitudeBase = 10.762622;
            double longitudeBase = 106.660172;
            DateTime createdDate = FormatDateTime(DateTime.Now);
            Random ran = new Random();

            string phonenumberHeader = "0908";
            string[] name = { "Brandon", "Christian", "Dylan", "Samuel", "Benjamin", "Nathan", "Zachary", "Logan", "Justin", "Gabriel", "Jose", "Austin", "Kevin", "Elijah", "Caleb" };
            string[] lastname = { "Lopez", "Lee", "Gonzalez", "Harriz", "Clark" };

            for (int i = 1; i < 26; i++)
            {
                DateTime pickingDate = createdDate.AddDays(i % 5);
                var request = new Request
                {
                    CreatedDate = createdDate,
                    PickingDate = pickingDate,
                    ExpectedDate = pickingDate.AddDays(i % 5 + 2),
                    PackageQuantity = i,
                    DeliveryLatitude = Math.Round(latitudeBase + i * 0.01, 6),
                    DeliveryLongitude = Math.Round(longitudeBase + i * 0.01, 6),
                    WareHouseId = i%5+1,
                    IssuerId = i + 76,
                    Status = "Inactive",
                    ReceiverName = name[ran.Next(0, 14)] + lastname[ran.Next(0, 4)],
                    ReceiverPhoneNumber = phonenumberHeader + ran.Next(100000, 999999).ToString(),
                    Address = "37 ĐT743C, Xã Bình Thắng, Dĩ An, Bình Dương, Vietnam",
                    Code = GenerateCode(createdDate, i + 76),
                    CustomerId = i + 76
                };
                dbContext.Add(request);
            }
            dbContext.SaveChanges();
        }

        //Add seed data of Shipment
        private static void SeedShipmentData(ApplicationDbContext dbContext)
        {
            for (int i = 0; i < 5; i++)
            {
                DateTime createdDate = FormatDateTime(DateTime.Now);
                long custormerID = (long)i + 77;
                var shipment = new Shipment
                {
                    RequestQuantity = i * 15,
                    CreatedDate = createdDate,
                    StartDate = createdDate,
                    EndDate = createdDate,
                    VehicleId = i + 1,
                    DriverId = i + 53,
                    CoordinatorId = i + 28,
                    Code = GenerateCode(createdDate, i + 28),
                    Status = "Pending"
                };
                dbContext.Add(shipment);
            }
            dbContext.SaveChanges();
        }

        //Add seed data of Shipment_Request
        private static void SeedShipmentRequestData(ApplicationDbContext dbContext)
        {
            Random ran = new Random();
            DateTime RequestEstimateDate = FormatDateTime(DateTime.Now);
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    var shipmentRequest = new ShipmentRequest
                    {
                        ShipmentId = i + 1,
                        RequestId = (i * 5) + j + 1,
                        RequestOrder = j + 1,
                        Note = "",
                        Status = "Waiting",
                        RequestEstimateDate = RequestEstimateDate.AddDays(ran.Next(0, i % 2 + 1)),
                        RequestDeliveriedDate = RequestEstimateDate.AddDays(ran.Next(2, i % 7 + 3))
                    };
                    dbContext.Add(shipmentRequest);
                    dbContext.SaveChanges();
                }
            }

        }
    }

}

