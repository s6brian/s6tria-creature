﻿using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class MechSetupManager : MonoBehaviour, IDropHandler
{
	[SerializeField] private Transform m_tinstalledChipsContainer;
	[SerializeField] private GameObject m_objMech;
	[SerializeField] private GameObject m_objArena;

	private static MechSetupManager m_instance = null;
	private int m_iCurrentChipInstalled;
	private int m_iMaxChipInstall = 5; // TODO: max should depend on current mech used
	private Dictionary<int, ChipData> m_dictChipData = new Dictionary<int, ChipData>();

	public static MechSetupManager instance {get{return m_instance;}}
	public Transform installedChipsContainer {get{return m_tinstalledChipsContainer;}}

	protected void OnEnable ()
	{
		DraggableChip.onEndDrag += DraggableChipOnEndDrag;
		PlayButton.onClick += OnClickPlay;
	}

	protected void OnDisable ()
	{
		DraggableChip.onEndDrag -= DraggableChipOnEndDrag;
		PlayButton.onClick -= OnClickPlay;
	}

	protected void Awake ()
	{
		if(m_instance == null){m_instance = this;}
		m_iCurrentChipInstalled = 0;
	}

	private void OnClickPlay ()
	{
		m_objArena.SetActive(true);
		m_objMech.SetActive(true);
		MechManager.instance.MechSetup(m_dictChipData);
	}

	private void DraggableChipOnEndDrag (DraggableChip p_selectedChip)
	{
		if(m_dictChipData.ContainsKey(p_selectedChip.chipDataDisplay.id))
		{
			m_dictChipData.Remove(p_selectedChip.chipDataDisplay.id);
		}
	}

	public void OnDrop (PointerEventData eventData)
	{
		m_iCurrentChipInstalled = m_tinstalledChipsContainer.childCount;

		if(m_iCurrentChipInstalled < m_iMaxChipInstall)
		{
			DraggableChip selectedChip = DraggableChip.selectedChip;
			selectedChip.transform.SetParent(m_tinstalledChipsContainer);
			// TODO: should update all sibling index to keep order
			selectedChip.transform.SetSiblingIndex(selectedChip.chipDataDisplay.id);
			m_dictChipData.Add(selectedChip.chipDataDisplay.id, selectedChip.chipDataDisplay.chipData);
		}
	}
}
