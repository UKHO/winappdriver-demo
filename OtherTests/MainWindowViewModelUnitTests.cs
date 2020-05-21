using NUnit.Framework;
using TestApp;

namespace OtherTests
{
    [TestFixture]
    public class MainWindowViewModelUnitTests
    {
        private MainWindowViewModel _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = new MainWindowViewModel();
        }

        [Test]
        public void ButtonText_should_be_Press_Me()
        {
            Assert.That(_sut.ButtonText, Is.EqualTo("Press me"));
        }

        [Test]
        public void ResultText_should_default_to_Not_Pressed()
        {
            Assert.That(_sut.ResultText, Is.EqualTo("Not pressed"));
        }

        [Test]
        public void ButtonCommand_execute_should_update_result_with_input()
        {
            _sut.InputText = "Hello";

            _sut.ButtonCommand.Execute(null);

            Assert.That(_sut.ResultText, Is.EqualTo("Input was Hello"));
        }
    }
}