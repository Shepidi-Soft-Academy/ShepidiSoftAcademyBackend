using MediatR;
using ShepidiSoft.API.Abstraction;

namespace ShepidiSoft.API.Controllers;


public class UploadsController(IMediator mediator) :  BaseApiController(mediator)
{

}
