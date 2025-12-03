using Zoo.BLL.Exceptions;
using Zoo.DAL.Repositories;
using Zoo.DL.Entities;

namespace Zoo.BLL.Services
{
    public class AddressService
    {
        private readonly AddressRepository _addressRepository;

        public AddressService(AddressRepository addressRepository) 
        {
            _addressRepository = addressRepository;
        }

        public void CreateAddress(Address address) 
        {
            _addressRepository.Add(address);
        }

        public Address GetAddress(int id) 
        {
            Address? address = _addressRepository.GetEntityById(id);
            if(address==null) 
            {
                throw new NotFoundException($"No Address found with Id:{id}");
            }
            return address;
        }
    }
}
