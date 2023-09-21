using ABSD.Application.ViewModels;
using System.Collections.Generic;

namespace ABSD.Application.Interfaces
{
    public interface IContentService
    {
        List<ContentViewModel> GetContentViewModel();

        int CreateContent(ContentViewModel contentViewModel);

        int UpdateContent(ContentViewModel contentViewModel);
    }
}