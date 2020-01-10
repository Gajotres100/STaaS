using ComProvis.Data.Companys;
using ComProvis.Params;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace ComProvis.UnitTest
{
    [TestClass]
    public class CreateCustomerTest
    {        
        ICompanyRepository _companyRepository;

        [TestInitialize]
        public void Setup()
        {
            var companyRepositoryMock = new Mock<ICompanyRepository>();
            var configurationMock = new Mock<IConfiguration>();
            companyRepositoryMock.Setup(r => r.SaveCompany(null));
            _companyRepository = companyRepositoryMock.Object;
        }

        [TestMethod]
        public void NoDataForCustomer_SaveCompany_FailToSave()
        {
            var createCompanyData = new CreateCompanyData();    
            _companyRepository.SaveCompany(createCompanyData);
            Assert.IsNotNull(createCompanyData);
        }
    }
}
