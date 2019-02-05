namespace Fti.Labs.Core.Constants
{
	using System;

	public static class Guids
	{
		public static class Application
		{
			public const string ApplicationGuidString = "fb501cc4-2f24-4256-af64-e3d4ac1c93e7";
			public static readonly Guid ApplicationGuid = Guid.Parse(ApplicationGuidString);
		}

		public static class Agent
		{
			public const string ManagerAgentGuidString = "328a8998-69db-4c50-a777-9e1443f91333";
			public const string WorkerAgentGuidString = "040cf59c-2449-4d2e-8f8b-d2b0e33947b5";

			public static readonly Guid ManagerAgentGuid = Guid.Parse(ManagerAgentGuidString);
			public static readonly Guid WorkerAgentGuid = Guid.Parse(WorkerAgentGuidString);
		}

        public static class EventHandlers
        {
            public const string JobConsoleString = "F1FE103F-B113-4D5C-8D6B-A305579D973E";
            public static readonly Guid JobConsole = Guid.Parse(JobConsoleString);

            public const string EncryptionPreSaveString = "1DFDB102-1BB0-4166-A41B-184269EC4312";

            public const string PostInstallEventHanlder = "81C86654-6EC7-40FC-BBD1-92EA7ECA181C";
        }

		public static class Fields
		{
			public static class Job
			{
				public static readonly Guid Status = Guid.Parse("8b48df20-83ef-4906-bab8-a9dcb1baef6c");
			}

			public static class Document
			{
				public static readonly Guid Email = Guid.Parse("c938dad0-07ce-417b-9a19-658ace01ea32");
			}
		}

        public static class Tabs
        {
            public const string WebAppString = "84FBF14F-A75D-42B5-A706-555853F3B475";
            public static Guid WebApp = Guid.Parse(WebAppString);
        }
	}
}
