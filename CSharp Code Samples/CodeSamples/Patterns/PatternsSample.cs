using CodeSamples.Patterns.Behavioral;
using CodeSamples.Patterns.Creational;

namespace CodeSamples.Patterns
{
    public class PatternsSample : SampleExecute
    {
        public override void Execute()
        {
            Title("PatternsSampleExecute");
            Section("Creational Patterns");
            var singleton = new SingletonPatternSample();
            singleton.Execute();
            var factoryMethod = new FactoryMethodPatternSample();
            factoryMethod.Execute();
            var abstractFactory = new AbstractFactoryPatternSample();
            abstractFactory.Execute();
            LineBreak();
            Section("Behavioral Patterns");
            var strategy = new StrategyPatternSample();
            strategy.Execute();
            var observer = new ObserverPatternSample();
            observer.Execute();
            var state = new StatePatternSample();
            state.Execute();
            Finish();
        }
    }
}
