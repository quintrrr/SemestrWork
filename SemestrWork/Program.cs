using BLL.Services;
using Core.DTO;
using Core.Interfaces;
using DAL.DAO;
using DAL.Repository;
using Mapping;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

namespace SemestrWork
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            var options = new DbContextOptionsBuilder<DAL.AppContext>()
            .UseNpgsql(ConfigurationManager.ConnectionStrings["AppConnection"].ConnectionString)
            .Options;
            DAL.AppContext ac = new DAL.AppContext(options);
            ITGroupRepository gr = new TGroupRepository(ac);
            ITPropertyRepository pr = new TPropertyRepository(ac);
            ITRelationRepository rr = new TRelationRepository(ac);

            IMapper<TGroupDTO, TGroup> gm = new GroupMapper();
            IMapper<TPropertyDTO, TProperty> pm = new PropertyMapper();
            IMapper<TRelationDTO, TRelation> rm = new RelationMapper();

            IGroupService gs = new GroupService(gr, rr, gm);
            IPropertyService ps = new PropertyService(pr, pm);
            IRelationService rs = new RelationService(rr, rm);
            Application.Run(new Form1(gs, ps, rs));
        }
    }
}