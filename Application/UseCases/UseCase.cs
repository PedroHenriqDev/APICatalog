using Application.Interfaces;
using AutoMapper;

namespace Application.UseCases;

public abstract class UseCase
{
    protected readonly IUnitOfWork _unitOfWork;
    protected readonly IMapper _mapper;

    public UseCase(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
}
