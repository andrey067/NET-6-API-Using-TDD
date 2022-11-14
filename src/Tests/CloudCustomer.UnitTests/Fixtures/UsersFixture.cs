using CloudCustomer.API.Domain;

namespace CloudCustomer.UnitTests.Fixtures
{
    public static class UsersFixture
    {
        public static List<User> GetTestsUsers() =>
        new(){
            new User{
                Id =1,
                    Name ="Teste",
                    Address = new Address(){
                                Street = "123 Main St",
                                City = "Casa",
                                ZipCode = "54324"
                            },
                    Email = "Audrey@gmail.com"
            },
            new User{
                Id =2,
                    Name ="Teste2",
                    Address = new Address(){
                                Street = "123 Main St",
                                City = "Casa",
                                ZipCode = "54324"
                            },
                    Email = "Audrey@gmail.com"
            },
            new User{
                Id =3,
                    Name ="Teste3",
                    Address = new Address(){
                                Street = "123 Main St",
                                City = "Casa",
                                ZipCode = "54324"
                            },
                    Email = "Audrey@gmail.com"
            },
        };
    }
}
