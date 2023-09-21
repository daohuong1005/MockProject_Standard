using ABSD.Application.Interfaces;
using ABSD.Application.ViewModels;
using ABSD.Data;
using ABSD.Data.Entities;
using ABSD.Data.Enums;
using ABSD.Data.IRepositories;
using System.Collections.Generic;
using System.Linq;

namespace ABSD.Application.Implements
{
    public class ContentService : IContentService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IContentRepository contentRepository;

        public ContentService(IUnitOfWork unitOfWork, IContentRepository contentRepository)
        {
            this.unitOfWork = unitOfWork;
            this.contentRepository = contentRepository;
        }

        public int CreateContent(ContentViewModel contentViewModel)
        {
            Content content = new Content()
            {
                Id = contentViewModel.Id,
                ContentName = contentViewModel.ContentName,
                Type = contentViewModel.Type,
            };

            contentRepository.Add(content);

            return unitOfWork.Commit();
        }

        public List<ContentViewModel> GetContentViewModel()
        {
            var query = contentRepository.GetAll(x => x.ContractContents).ToList();
            List<ContentViewModel> contentViewModelsList = new List<ContentViewModel>();
            foreach (var item in query)
            {
                var contentViewModel = new ContentViewModel();
                contentViewModel.Id = item.Id;
                contentViewModel.ContentName = item.ContentName;
                contentViewModel.Type = item.Type;
                contentViewModel.TypeName = GeneralDictionary.Content.Single(c => c.Key == item.Type).Value.ToString();

                contentViewModelsList.Add(contentViewModel);
            }

            return contentViewModelsList;
        }

        public int UpdateContent(ContentViewModel contentViewModel)
        {
            Content content = contentRepository.Single(x => x.Id == contentViewModel.Id);
            content.ContentName = contentViewModel.ContentName;
            content.Type = contentViewModel.Type;

            contentRepository.Update(content);

            return unitOfWork.Commit();
        }
    }
}