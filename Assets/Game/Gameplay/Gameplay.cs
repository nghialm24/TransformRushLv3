using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Funzilla
{
	internal class Gameplay : Scene
	{
		[SerializeField] public List<GameObject> levelList = new List<GameObject>();
		[SerializeField] private PlayerController playerController;
		[SerializeField] private GameObject HomeUI;
		[SerializeField] private GameObject PlayUI;
		[SerializeField] private GameObject FightUI;
		[SerializeField] private GameObject WinUI;
		[SerializeField] private GameObject LoseUI;
		[SerializeField] GameObject confetti;
		[SerializeField] static int currentLevel = 0;

		private enum State
		{
			Init,
			Play,
			Win,
			Lose
		}

		private State _state = State.Init;
		
		private void ChangeState(State newState)
		{
			if (_state == newState) return;
			ExitOldState();
			_state = newState;
			EnterNewState();
		}

		private void EnterNewState()
		{
			switch (_state)
			{
				case State.Init:
					break;
				case State.Play:
					break;
				case State.Win:
					break;
				case State.Lose:
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		private void ExitOldState()
		{
			switch (_state)
			{
				case State.Init:
					break;
				case State.Play:
					break;
				case State.Win:
					break;
				case State.Lose:
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		private void Start()
		{
			levelList[currentLevel].SetActive(true);
		}
		private void Update()
		{
			
		#if UNITY_EDITOR
			if (Input.GetKeyDown(KeyCode.R))
			{
				SceneManager.ReloadScene(SceneID.Gameplay);
			}
		#endif
			
			if (playerController.Win)
			{
				ChangeState(State.Win);
			}
			if (playerController.Lose)
			{
				ChangeState(State.Lose);
			}

			switch (_state)
			{
				case State.Init:
					SceneManager.HideLoading();
					SceneManager.HideSplash();
					if (Input.GetMouseButton(0))
					{
						ChangeState(State.Play);
						PlayUI.SetActive(true);
						HomeUI.SetActive(false);
					}
					break;
				case State.Play:
					if (playerController.isFightingEnemy || playerController.isFightingBoss)
					{
						PlayUI.SetActive(false);
						FightUI.SetActive(true);
					}
					if(playerController.FightingWin) 
					{
						PlayUI.SetActive(true);
						FightUI.SetActive(false);
					}
					break;
				case State.Win:
					FightUI.SetActive(false);
					confetti.SetActive(true);
					if (Input.GetMouseButtonDown(1))
					{
						currentLevel++;
						
						SceneManager.ReloadScene(SceneID.Gameplay);
					}
					LoadUI();
					break;
				case State.Lose:
					StartCoroutine(ExampleCoroutine());
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		// public void BtnNext()
		// {
		// 	Scene scene = SceneManager.GetActiveScene();
		// 	SceneManager.LoadScene("Gameplay2");
		// }
		internal void LoadUI()
		{
			StartCoroutine(DelayUILoad());
		}
		IEnumerator DelayUILoad()
		{
			yield return new WaitForSeconds(1f);
			WinUI.SetActive(true);
		}
		IEnumerator ExampleCoroutine()
		{
			yield return new WaitForSeconds(1f);
			FightUI.SetActive(false);
			LoseUI.SetActive(true);
		}
	}
}