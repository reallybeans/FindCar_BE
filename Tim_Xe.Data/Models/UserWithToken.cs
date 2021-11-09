using Tim_Xe.Data.Repository.Entities;

namespace Tim_Xe.Data.Models
{
    public class UserWithToken : LoginDTO
    {
        public string AccessToken { get; set; }
        public UserWithToken(Manager manager, Driver driver, Customer customer)
        {
            if (manager != null)
            {
                this.Id = manager.Id;
                this.Email = manager.Email;
                this.Name = manager.Name;
                this.Phone = manager.Phone;
                this.Img = manager.Img;
                this.CardId = manager.CardId;
                this.Status = manager.Status;

                this.Role = manager.Role.Name;
            }
            else if (customer != null)
            {
                this.Id = customer.Id;
                this.Email = customer.Email;
                this.Name = customer.Name;
                this.Phone = customer.Phone;
                this.Img = customer.Img;
                this.Status = customer.Status;

                this.Role = "customer";
            }
            else
            {
                this.Id = driver.Id;
                this.Email = driver.Email;
                this.Name = driver.Name;
                this.Phone = driver.Phone;
                this.Img = driver.Img;
                this.CardId = driver.CardId;
                this.Status = driver.Status;

                this.Role = "driver";
            }

        }
    }
}
