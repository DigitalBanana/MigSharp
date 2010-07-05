using MigSharp.Process;

using NUnit.Framework;

using Rhino.Mocks;

namespace MigSharp.NUnit.Process
{
    [TestFixture, Category("Smoke")]
    public class MigrationBatchTests
    {
        [Test]
        public void VerifyStepExecutedIsRaised()
        {
            IMigrationStep step1 = MockRepository.GenerateStub<IMigrationStep>();
            step1.Expect(s => s.Metadata).Return(new Metadata1());
            IMigrationStep[] steps = new[]
            {
                step1,
            };
            IVersioning versioning = MockRepository.GenerateStub<IVersioning>();
            MigrationBatch batch = new MigrationBatch(steps, steps, versioning);
            int count = 0;
            batch.StepExecuted += (sender, args) => count++;
            batch.Execute();
            Assert.AreEqual(2 * steps.Length, count);
        }

        private class Metadata1 : IMigrationMetadata
        {
            public string Tag { get { return null; } }
            public string ModuleName { get { return string.Empty; } }
            public long Timestamp { get { return 1; } }
        }
    }
}