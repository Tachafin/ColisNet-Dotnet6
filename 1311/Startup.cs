using _1311.Controllers;
using _1311.Models;
using _1311.Models.Repository;
using _1311.Models.Repository.Admin;
using _1311.Models.Repository.IAccountRepositorys;
using _1311.Models.Repository.IBonsLivraisonRepositorys;
using _1311.Models.Repository.IBoutiqueRepositorys;
using _1311.Models.Repository.IColisRepositorys;
using _1311.Models.Repository.ICommentRepositorys;
using _1311.Models.Repository.IDemandeRepositorys;
using _1311.Models.Repository.IfactureRepositorys;
using _1311.Models.Repository.IReclamationRepositorys;
using _1311.Models.Repository.IStatistiqueRepositorys;
using _1311.Models.Repository.IUserRepositorys;
using _1311.Models.Repository.IVilleRepositorys;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _1311
{
    public class Startup
    {
        IConfiguration _configuration;
        public Startup(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            
            services.AddIdentity<AppUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>();
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(_configuration.GetConnectionString("LivraisonDbConnection")));
            services.AddMvc(
             options => {
                 options.EnableEndpointRouting = false;
                 var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
                 options.Filters.Add(new AuthorizeFilter(policy));
             });
            services.AddControllersWithViews();

            services.ConfigureApplicationCookie(
                config =>
                {
                    config.LoginPath = "/admin/colis";

                }

                );





            services.AddScoped < IColisRepository<Colis>, ColisRepositoryy>();

            //services.AddScoped<GlobalRepository<Colis>, ColisRepository>();
            services.AddScoped<ICommentRepositorys<Comment>, CommentRepository>();
            services.AddScoped<GlobalRepository<BonsLivraison>, BonsLivraisonRepository>();
            services.AddScoped<IBonsLivraisponRepository<BonsLivraison>, BonsLivraisonRepositorys>();
            services.AddScoped<IVilleRepository<Ville>, VilleRepository>();
            services.AddScoped<IListRamassageRepository<ListeRamassage>, ListeRamassagesRepository>();
            services.AddScoped<GlobalRepository<Reclamation>, ReclamationRepository>();
            services.AddScoped<GlobalRepository<Demande>, DemandeRepository>();
            services.AddScoped<AdminRepository<Colis>, ColisAdminRepository>();
            services.AddScoped<ILivreurRepository<Livreur>, LivreurRepository>();
            services.AddScoped<IDemandeRepository<Demande>, DemandeRepositoryy>();
            services.AddScoped<IReclamationRepository<Reclamation>, ReclamationRepositoryy>();
            services.AddScoped<IBoutiqueRepositoryy<Boutique>, BoutiqueRepository>();
            services.AddScoped<IFactureRepository<Facture>, FactureRepository>();
            services.AddScoped<IstatistiqueRepository<Colis>, StatistiqueRepository>();
            services.AddScoped<IAccountRepository, AccountRepository>();

            services.AddScoped<CurrentUser>();
            services.AddScoped<UserRepository>();

            //services.AddScoped<IstatistiqueRepository<Livreur>, StatistiqueRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else 
            {
                app.UseExceptionHandler("/Error");
                app.UseStatusCodePagesWithReExecute("/Error/{0}");
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers(); // This maps all controller actions with attribute routing
            });
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=User}/{action=Index}/{id?}");
            });

        }
    }
}
