using Application.Interfaces;
using AutoMapper;

namespace Application.Providers;

public abstract class BaseProvider
{
    protected readonly IUnitOfWork _unitOfWork;
    protected readonly IMapper _mapper;

    public BaseProvider(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
}
