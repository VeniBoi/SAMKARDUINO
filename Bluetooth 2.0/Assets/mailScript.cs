using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
using UnityEngine.UI;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

public class mailScript : MonoBehaviour {

	public InputField path, youremail, subject, message;
	public Text error;
	static public bool nameChosen = false;


	public void sendmail_Start()
	{
		StartCoroutine(sendmail());
	}

	public IEnumerator sendmail()
	{
		yield return new WaitForSeconds(0.0f);
		if (subject.text == "" || youremail.text == "")
		{
			error.text = "Please fill in your email and patients name";
		}
		if (!nameChosen)
		{
			error.text = "Please choose a patient";
		}
		else
		{
			MailMessage mail = new MailMessage();

			//Kuka lähettää
			mail.From = new MailAddress(youremail.text);

			//Mihin lähetetään
			mail.To.Add(youremail.text);	

			//Aihe
			mail.Subject = "Data from " + subject.text;

			//Body eli pääteksti
			mail.Body = "THIS IS AN AUTOMATED MESSAGE, PLEASE DO NOT REPLY!" + "\n ------------------------------------------------------\n \n" + "You can find the files of " + subject.text + " attached.";

			if (nameChosen == true)
			{
				Attachment attachment = null;
				attachment = new Attachment(Application.persistentDataPath + "/datakansio/" + buttonListButton.nimi + ".txt");
				mail.Attachments.Add(attachment);
			}

			SmtpClient smtpServer = new SmtpClient("smtp.gmail.com");
			smtpServer.Port = 587;
			smtpServer.Credentials = new System.Net.NetworkCredential("SmartChairData@gmail.com", "Smartchair2018") as ICredentialsByHost;
			smtpServer.EnableSsl = true;
			ServicePointManager.ServerCertificateValidationCallback =
				delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
			
			smtpServer.Send(mail);
			error.text = "The mail has been sent!";
			nameChosen = false;

			 
		}
	}
}
