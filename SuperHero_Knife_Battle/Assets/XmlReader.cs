using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System;
using UnityEngine.UI;

public class XmlReader : MonoBehaviour // the Class
{
	public GameObject mainScreen;
	XmlDocument PresentXml;
	public  RawImage ThumbnailImage;

	public TextAsset GameAsset;
	public string VideoUrl;
	public string ThumbnailUrl;
	Texture2D AdImage;
	public GameObject AdButton;

	List<Dictionary<string,string>> levels = new List<Dictionary<string,string>>();
	Dictionary<string,string> obj;

	public Text Info;


	void Start()
	{	
		ThumbnailImage.gameObject.SetActive (false);
		AdButton.SetActive (false);
		GetXmlfromOnline();
	}

	public void GetXmlfromOnline()
	{
		//GameAsset = Resources.Load("categories") as TextAsset;
		ServicePointManager.ServerCertificateValidationCallback = MyRemoteCertificateValidationCallback;
		string m_strFilePath = "https://s3.us-east-2.amazonaws.com/rajvideolinks/Videos.xml";
		XmlDocument myXmlDocument = new XmlDocument();
		myXmlDocument.Load(m_strFilePath); //Load NOT LoadXml
		Debug.Log(myXmlDocument.InnerXml);
		XmlNodeList ListOfNodes = myXmlDocument.GetElementsByTagName ("level");
		Debug.Log ("List of nodes :" + ListOfNodes.Count);
		Info.text = "List of nodes :" + ListOfNodes.Count.ToString ();

		       foreach (XmlNode levelInfo in ListOfNodes)
					{
						XmlNodeList levelcontent = levelInfo.ChildNodes;
						obj = new Dictionary<string,string>(); // Create a object(Dictionary) to colect the both nodes inside the level node and then put into levels[] array.
			
						foreach (XmlNode levelsItens in levelcontent) // levels itens nodes.
						{
				             if(levelsItens.Name == "Link")
				              {
					            Debug.Log("444444444444444 :" + levelsItens.InnerText);
					            Info.text = "444444444444444 :" + levelsItens.InnerText;
					            //Application.OpenURL (levelsItens.InnerText);
					            VideoUrl = levelsItens.InnerText;
				              }

				             if(levelsItens.Name == "IMG")
				              {
					            Debug.Log("444444444444444 :" + levelsItens.InnerText);
					            Info.text = "444444444444444 :" + levelsItens.InnerText;
					            //StartCoroutine (GetVideoThumbnail (levelsItens.InnerText));
					             StartCoroutine (GetThumbnailAsImage (levelsItens.InnerText));

				               }
						}
			
					}
			}


	public bool MyRemoteCertificateValidationCallback(System.Object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) {
		bool isOk = true;
		// If there are errors in the certificate chain, look at each error to determine the cause.
		if (sslPolicyErrors != SslPolicyErrors.None) {
			for (int i=0; i<chain.ChainStatus.Length; i++) {
				if (chain.ChainStatus [i].Status != X509ChainStatusFlags.RevocationStatusUnknown) {
					chain.ChainPolicy.RevocationFlag = X509RevocationFlag.EntireChain;
					chain.ChainPolicy.RevocationMode = X509RevocationMode.Online;
					chain.ChainPolicy.UrlRetrievalTimeout = new TimeSpan (0, 1, 0);
					chain.ChainPolicy.VerificationFlags = X509VerificationFlags.AllFlags;
					bool chainIsValid = chain.Build ((X509Certificate2)certificate);
					if (!chainIsValid) {
						isOk = false;
					}
				}
			}
		}
		return isOk;
	}

	IEnumerator GetXml()
	{
		string url = "https://s3.us-east-2.amazonaws.com/rajtestroku/roku/Unity.xml";
		WWW www = new WWW(url);
		yield return www;
		Debug.Log ("typeeeeeeeeeeee" + www.text);
		//GameAsset = www.text;
		if (GameAsset) {
			Debug.Log ("yessssssssssssssssssss :" + GameAsset.name);
		} else {
			Debug.Log ("noooooooooooooooooooo :");
		}
	}


	IEnumerator GetXmlFromServer()
	{
		Renderer renderer = mainScreen.GetComponent<Renderer>();

		string url = "https://s3.us-east-2.amazonaws.com/rajvideolinks/Logo.png";
		WWW www = new WWW(url);
		yield return www;
		Texture2D image = www.texture as Texture2D;
		if (image == null) {
			Debug.Log ("Nooooooooooo");
		} else {
			Debug.Log ("Yessssssssssssssss");
			renderer.material.mainTexture = image;
		}

	}

	IEnumerator GetVideoThumbnail(string Link)
	{
		Renderer renderer = mainScreen.GetComponent<Renderer>();

		string url = Link;
		ThumbnailUrl = Link;
		WWW www = new WWW(url);
		yield return www;
		Texture2D image = www.texture as Texture2D;
		if (image == null) {
			Debug.Log ("Nooooooooooo");
		} else {
			Debug.Log ("Yessssssssssssssss");
			renderer.material.mainTexture = image;
		}
	}

	IEnumerator GetThumbnailAsImage(string Link)
	{
		Info.text = "444444444444444 :" + Link;
		string url = Link;
		ThumbnailUrl = Link;
		WWW www = new WWW(url);
		yield return www;
		AdImage = www.texture as Texture2D;
		PlaceImage ();
	}

	public void PlaceImage()
	{
		ThumbnailImage.gameObject.SetActive (true);
		AdButton.SetActive (true);
		ThumbnailImage.texture = AdImage;
	}

	IEnumerator PlayVideo()
	{
		Renderer renderer = mainScreen.GetComponent<Renderer>();

		string url = "https://s3.us-east-2.amazonaws.com/rajtestroku/roku/Images/Edited/1.jpg";
		WWW www = new WWW(url);
		yield return www;
		Texture2D image = www.texture as Texture2D;
		if (image == null) {
			Debug.Log ("Nooooooooooo");
		} else {
			Debug.Log ("Yessssssssssssssss");
			renderer.material.mainTexture = image;
		}
	}

	public void PlayVideoFromChannel()
	{
		Debug.Log ("Play Video");
		if(ThumbnailUrl !="")
		{
			Application.OpenURL(VideoUrl);
		}
	}

}