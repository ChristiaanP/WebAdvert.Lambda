using AdvertApi.Models.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebAdvert.SearchWorker
{
    public static class MappingHelper
    {
        public static AdvertType MapAdvert(AdvertConfirmedMessage message)
        {
            var doc = new AdvertType
            {

                Id = message.Id,
                Title = message.Title,
                CreationDateTime = DateTime.Now

            };

            return doc;

        }

    }
}
