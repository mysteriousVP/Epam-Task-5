using AutoMapper;
using BLL.App_Configuration_Mapper;
using BLL.DTO;
using BLL.Interfaces;

// ado.net task
using DAL_EF;
using DAL_EF.Entities;
using DAL_EF.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class ProviderService : IProviderService
    {
        private readonly IUnitOfWork UnitOFWork;
        private readonly IMapper Mapper;

        public ProviderService(IUnitOfWork uow)
        {
            this.UnitOFWork = uow ?? throw new NullReferenceException();
            this.Mapper = MappingConfiguration.ConfigureMapper().CreateMapper();
        }

        public ProviderService()
        {
            this.UnitOFWork = new UnitOfWork();
            this.Mapper = MappingConfiguration.ConfigureMapper().CreateMapper();
        }

        public bool Add(ProviderDTO item)
        {
            Provider newProvider = Mapper.Map<ProviderDTO, Provider>(item);

            UnitOFWork.Providers.Create(newProvider);

            if (UnitOFWork.SaveChanges())
            {
                return true;
            }
            return false;
        }

        public async Task<ProviderDTO> Get(int id)
        {
            Provider provider = await UnitOFWork.Providers.Get(id);

            return Mapper.Map<Provider, ProviderDTO>(provider);
        }

        public async Task<IEnumerable<ProviderDTO>> GetAll()
        {
            return Mapper.Map<IEnumerable<Provider>, IEnumerable<ProviderDTO>>(await UnitOFWork.Providers.GetAll());
        }

        public bool Remove(ProviderDTO item)
        {
            UnitOFWork.Providers.Remove(Mapper.Map<ProviderDTO, Provider>(item));

            if (!UnitOFWork.SaveChanges())
            {
                return false;
            }

            return true;
        }

        public async Task<bool> Remove(int id)
        {
            Provider provider = await UnitOFWork.Providers.Get(id);

            if (provider == null)
            {
                return false;
            }

            UnitOFWork.Providers.Remove(provider);

            if (!UnitOFWork.SaveChanges())
            {
                return false;
            }

            return true;
        }

        public bool Update(ProviderDTO item)
        {
            Provider provider = Mapper.Map<ProviderDTO, Provider>(item);
            UnitOFWork.Providers.Update(provider);

            if (!UnitOFWork.SaveChanges())
            {
                return false;
            }

            return true;
        }

        public async Task<IEnumerable<ProviderDTO>> GetProvidersByCategory(int categoryId)
        {
            IEnumerable<Product> products = await UnitOFWork.Products.GetAll();
            IEnumerable<int> providersIds = products.Where(p => p.CategoryId == categoryId).Select(p => p.ProviderId);

            List<ProviderDTO> selectedProviders = new List<ProviderDTO>();
            foreach(int id in providersIds)
            {
                selectedProviders.Add(Mapper.Map<Provider, ProviderDTO>(await UnitOFWork.Providers.Get(id)));
            }

            return selectedProviders;
        }
    }
}
