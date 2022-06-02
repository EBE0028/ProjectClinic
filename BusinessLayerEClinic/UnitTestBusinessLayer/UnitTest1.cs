using BusinessLayerEClinic;
namespace UnitTestBusinessLayer
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void ValidateLoginPass()
        {
            BusinessLogic Obj= new BusinessLogic();
            int res=Obj.UserNamePasswordValidation("Samy", "Samy@123");
            Assert.AreEqual(1,res);
        }

        [TestMethod]
        public void ValidateLoginFail()
        {
            BusinessLogic Obj = new BusinessLogic();
            int res = Obj.UserNamePasswordValidation("James", "JamesKora");
            Assert.AreEqual(0, res);
        }

        [TestMethod]
        public void AddPatientPass()
        {
            BusinessLogic Obj=new BusinessLogic();
            DateTime dateTime= new DateTime(1998,31,11);
            String.Format("{0:d/M/yyyy}", dateTime);
            int res=Obj.AddPatient("Jerro", "Osan", "M", dateTime);
            Assert.AreEqual(1,res);
        }

        [TestMethod]
        public void AddPatientFail()
        {
            BusinessLogic Obj = new BusinessLogic();
            DateTime dateTime = new DateTime(1898, 5, 5);
            String.Format("{0:d/M/yyyy}", dateTime);
            int res = Obj.AddPatient("Jerro", "Osan", "M", dateTime);
            Assert.AreEqual(0, res);
        }

    }
}