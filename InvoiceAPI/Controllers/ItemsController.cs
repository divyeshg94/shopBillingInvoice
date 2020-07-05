using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using invoiceGenerator.PersistenceSql;
using InvoiceGenerator.Models;

namespace InvoiceAPI.Controllers
{
    [RoutePrefix("items")]
    public class ItemsController : ApiController
    {
        // GET: customer
        [HttpGet]
        [Route("")]
        public List<ItemsModel> GetAll()
        {
            return Item.GetAllItems();
        }

        [HttpGet]
        [Route("name")]
        public ItemsModel Get(string name = "", string category = "")
        {
            return Item.GetItem(name, category);
        }

        [HttpPost]
        public async Task Add(ItemsModel item)
        {
            await Item.AddItem(item);
        }

        [HttpPut]
        public void Update(ItemsModel item)
        {
            Item.UpdateItem(item);
        }
    }
}