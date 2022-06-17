using System.Collections.Generic;
using UnityEngine;

#if !UNITY_EDITOR && REMOTE_ENABLED
using GameAnalyticsSDK;
#endif

namespace Funzilla
{
	internal enum ExperimentType
	{
		Origin,
		NewFeature
	}

	internal class ExperimentManager
	{
		internal static List<ExperimentType> Experiments { get; } = new List<ExperimentType>
		{
			ExperimentType.Origin,
			ExperimentType.NewFeature,
		};

		private ExperimentType _activeExperiment;
		internal static ExperimentType ActiveExperiment => Instance._activeExperiment;
		internal bool NewFeatureEnabled => ActiveExperiment == ExperimentType.NewFeature;

		private static ExperimentManager _instance;

		private static ExperimentManager Instance
		{
			get
			{
				_instance ??= new ExperimentManager();
				return _instance;
			}
		}

		private ExperimentManager()
		{
			var experimentName = "Test_" + Application.version;
#if !UNITY_EDITOR && REMOTE_ENABLED
			var s = GameAnalytics.GetRemoteConfigsValueAsString(experimentName, "0");
			int.TryParse(s, out var experiment);
			if (experiment < 0 || experiment >= Experiments.Count)
			{
				experiment = 0;
			}
#else
			var experiment = PlayerPrefs.GetInt(experimentName, -1);
			if (experiment < 0 || experiment >= Experiments.Count)
			{
				experiment = Random.Range(0, Experiments.Count);
				PlayerPrefs.SetInt(experimentName, experiment);
			}
#endif

			_activeExperiment = Experiments[experiment];
			var logEvent = $"{experimentName}_{experiment}";
			Analytics.LogEvent(logEvent);
		}

		internal static void ChangeTest(ExperimentType experiment)
		{
			Instance._activeExperiment = experiment;
			SceneManager.ReloadScene(SceneID.Gameplay);
			// TODO: Handle necessary test switching here
		}
	}
}