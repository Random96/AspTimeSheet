using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AspTimeSheet.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AutoMapper;

namespace AspTimeSheet
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDefaultIdentity<IdentityUser>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            // Auto Mapper Configurations
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Hooky, Models.HookyModelView>()
                .ForMember(x => x.PositionName, opt => opt.MapFrom(y=>y.Position.Name))
                .ForMember(x => x.PersonName, opt => opt.MapFrom(y => y.Person.Name))
                .ForMember(x => x.PersonMiddleName, opt => opt.MapFrom(y => y.Person.MiddleName))
                .ForMember(x => x.PersonLastName, opt => opt.MapFrom(y => y.Person.LastName));

            CreateMap<Models.HookyModelView, Hooky>();

            CreateMap<Models.PersonModelView, Person>();
            CreateMap<Person, Models.PersonModelView>();

            CreateMap<Hooky, Models.HookyFilterModelView>()
                .ForMember(x => x.PositionName, opt => opt.MapFrom(y => y.Position.Name))
                .ForMember(x => x.PersonName, opt => opt.MapFrom(y => y.Person.Name))
                .ForMember(x => x.PersonMiddleName, opt => opt.MapFrom(y => y.Person.MiddleName))
                .ForMember(x => x.PersonLastName, opt => opt.MapFrom(y => y.Person.LastName))
                .ForMember(x => x.CommentOperation, opt => opt.Ignore())
                .ForMember(x => x.HookyDateOperation, opt => opt.Ignore())
                .ForMember(x => x.HookyTimeOperation, opt => opt.Ignore())
                .ForMember(x => x.PersonLastNameOperation, opt => opt.Ignore())
                .ForMember(x => x.PersonMiddleNameOperation, opt => opt.Ignore())
                .ForMember(x => x.PersonNameOperation, opt => opt.Ignore())
                .ForMember(x => x.PositionOperation, opt => opt.Ignore());

            CreateMap<Data.StaffPosition, Models.StaffPositionModelView>()
                .ForMember(x => x.Checked, opt => opt.Ignore());
        }
    }
}
