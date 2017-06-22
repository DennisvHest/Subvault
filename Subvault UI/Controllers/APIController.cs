using Subvault_Domain.Entities;
using Subvault_UI.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Subvault_UI.Controllers {

    public class APIController : ApiController {

        private ItemManager itemManager;

        public APIController(ItemManager itemManager) {
            this.itemManager = itemManager;
        }

        [HttpGet]
        public Item[] Search(string query) {
            return itemManager.Search(query).ToArray();
        }
    }
}
