using Domain.Dtos;
using Domain.Entities;
using Domain.Repositories;

namespace Domain.Services
{
    public class CostumerService : ICostumerService
    {
        private readonly ICostumerRepository _costumerRepository;
        public CostumerService(ICostumerRepository costumerRepository)
        {
            _costumerRepository = costumerRepository;
        }

        public async Task CreateOrUpdateCostumerAsync(CreateUpdateCostumerDto request, CancellationToken cancellationToken)
        {
            //TODO: Implement domain validations and throw a domain exception
            //TODO: Add data annotations to the CreateUpdateCostumerDto instead of doing the validation here
            if (request is null || request.Email is null)
                throw new ArgumentNullException(nameof(request));

            Costumer costumer = await _costumerRepository.GetByEmailAsync(request.Email);

            if(costumer is null)
            {
                costumer = new Costumer(request.Name, request.Email);
                await _costumerRepository.AddAsync(costumer);
            }
            else
            {
                costumer.Name = request.Name;
                await _costumerRepository.UpdateAsync(costumer);
            }

            await _costumerRepository.SaveChangesAsync();
        }
    }
}
