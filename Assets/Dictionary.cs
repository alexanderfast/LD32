using UnityEngine;
using System.Linq;
using System.Collections;

public static class Dictionary
{
	private static string[] m_words;

	public static string[] Words
	{
		get
		{
			if (m_words == null)
			{
				var text = (TextAsset)Resources.Load("Words");
				m_words = text.text.Split('\n').Select(x => x.Trim()).ToArray();
			}
			return m_words;
		}
	}
}