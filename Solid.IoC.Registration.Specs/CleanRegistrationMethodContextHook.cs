using TechTalk.SpecFlow;

namespace Solid.IoC.Registration.Specs
{
    [Binding]
    public sealed class CleanRegistrationMethodContextHook
    {
        [AfterScenario("cleanRegistrationMethodContext")]
        public void AfterScenario()
        {
            RegistrationMethodContext.ClearRegistrations();
        }
    }
}
