using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RenderHeads.Media.AVProVideo;
using Google.Play.AssetDelivery;
using UnityEngine.Video;
using System.IO;
//using System;

// #SCRIPT PARA TRANSFORMAR .mp4 a .mp4.bytes!!!!
// for file in *.mp4; do mv "$file" "${file/.mp4/.mp4.bytes}"; done
// #agregar en archivo de texto, ejecutar como sh desde cmd

public class packageManager : MonoBehaviour
{
    MediaPlayer MediaPlayer1;
    AssetBundle packageBundle1;
    AssetBundle temporalBundle;
    Text Messages;
    string assetsPath;
    string assetBundleProjectDirectory;
    string assetBundleHierarchyPath;
    GameObject PackButtonPrefab;
    GameObject VideoButtonPrefab;
    RectTransform bundleContent;
    RectTransform assetContent;

    Dictionary<string, List<string>> assetsInBundles = new Dictionary<string, List<string>>();
    Dictionary<string, bool> downloadedAssetBundles = new Dictionary<string, bool>()
    {
        {"videopackage1", false},
        {"videopackage2", false},
        {"videopackage3", false},
        {"videopackage4", false},
        {"videopackage5", false},
        {"videopackage6", false},
        {"videopackage7", false},
        {"videopackage8", false},
        {"videopackage9", false},
        {"videopackage10", false},
        {"videopackage11", false},
        {"videopackage12", false},
        {"videopackage13", false}
    };


    // Start is called before the first frame update
    void Start()
    {

        // se referencia a objetos en escena
        MediaPlayer1 = GameObject.Find("MediaPlayer1").GetComponent<MediaPlayer>();
        Messages = GameObject.Find("messages").GetComponent<Text>();
        bundleContent = GameObject.Find("BundlesContent").GetComponent<RectTransform>();
        assetContent = GameObject.Find("AssetsContent").GetComponent<RectTransform>();

        PackButtonPrefab = Resources.Load("PackButton") as GameObject;
        VideoButtonPrefab = Resources.Load("VideoButton") as GameObject;

        // direccion en la que se almacenan los assets 
        assetsPath = Application.persistentDataPath;// + "/assets";
        assetBundleProjectDirectory = "assets/bundledassets"; // por alguna razon en minusculas ??, en el proyecto es /Assets/BundledAssets/ 
        assetBundleHierarchyPath = assetsPath + "/assetBundleHierarchy.txt";


        // se crean los directorios de assets
        buildAssetsDirectory();
        
        // se lee el archivo de resumen de paquetes en caso de existir y se lee la lista de assets en cada bundle previamente descargado
        if (File.Exists(assetBundleHierarchyPath))
        {
            loadDownloadedAssetBundlesFileAndLoadAssetsInBundlesFile();
        }

        // se rellena el scrollview de bundles
        populateBundleScrollView();
        populateAssetScrollView("", false);


    }

    public void playVideoAsset(string assetName, string assetBundleName)
    {
        // correr video :)
        if (downloadedAssetBundles[assetBundleName])
        {
            Messages.text = "trying to play: " + assetsPath + "/"  + assetName;
            MediaPlayer1.OpenMedia(MediaPathType.RelativeToPersistentDataFolder, assetsPath + "/"  + assetName);
            MediaPlayer1.Play();
            Messages.text = "playing: " + assetsPath + "/" + assetName;
        }
        else // no correr video :C   
        {
            
        }
        
    }

    private void buildAssetsDirectory()
    {
        // directorios videos
        if (!Directory.Exists(assetsPath + "/assets/bundledassets"))
            Directory.CreateDirectory(assetsPath + "/assets/bundledassets");
        if (!Directory.Exists(assetsPath + "/assets/bundledassets/videos"))
            Directory.CreateDirectory(assetsPath + "/assets/bundledassets/videos");
        if (!Directory.Exists(assetsPath + "/assets/bundledassets/videos/advervios"))
            Directory.CreateDirectory(assetsPath + "/assets/bundledassets/videos/advervios");
        if (!Directory.Exists(assetsPath + "/assets/bundledassets/videos/fondos"))
            Directory.CreateDirectory(assetsPath + "/assets/bundledassets/videos/fondos");
        if (!Directory.Exists(assetsPath + "/assets/bundledassets/videos/letras"))
            Directory.CreateDirectory(assetsPath + "/assets/bundledassets/videos/letras");
        if (!Directory.Exists(assetsPath + "/assets/bundledassets/videos/objetos-animales"))
            Directory.CreateDirectory(assetsPath + "/assets/bundledassets/videos/objetos-animales");
        if (!Directory.Exists(assetsPath + "/assets/bundledassets/videos/objetos-animales/otros"))
            Directory.CreateDirectory(assetsPath + "/assets/bundledassets/videos/objetos-animales/otros");
        if (!Directory.Exists(assetsPath + "/assets/bundledassets/videos/otros"))
            Directory.CreateDirectory(assetsPath + "/assets/bundledassets/videos/otros");
        if (!Directory.Exists(assetsPath + "/assets/bundledassets/videos/prueba"))
            Directory.CreateDirectory(assetsPath + "/assets/bundledassets/videos/prueba");
        if (!Directory.Exists(assetsPath + "/assets/bundledassets/videos/profesora"))
            Directory.CreateDirectory(assetsPath + "/assets/bundledassets/videos/profesora");
        if (!Directory.Exists(assetsPath + "/assets/bundledassets/videos/profesora/acierto"))
            Directory.CreateDirectory(assetsPath + "/assets/bundledassets/videos/profesora/acierto");
        if (!Directory.Exists(assetsPath + "/assets/bundledassets/videos/profesora/aprendizaje"))
            Directory.CreateDirectory(assetsPath + "/assets/bundledassets/videos/profesora/aprendizaje");
        if (!Directory.Exists(assetsPath + "/assets/bundledassets/videos/profesora/error"))
            Directory.CreateDirectory(assetsPath + "/assets/bundledassets/videos/profesora/error");
        if (!Directory.Exists(assetsPath + "/assets/bundledassets/videos/profesora/evaluacion"))
            Directory.CreateDirectory(assetsPath + "/assets/bundledassets/videos/profesora/evaluacion");
        if (!Directory.Exists(assetsPath + "/assets/bundledassets/videos/profesora/recordatorio"))
            Directory.CreateDirectory(assetsPath + "/assets/bundledassets/videos/profesora/recordatorio");
        if (!Directory.Exists(assetsPath + "/assets/bundledassets/videos/profesora/recordatorio/lados"))
            Directory.CreateDirectory(assetsPath + "/assets/bundledassets/videos/profesora/recordatorio/lados");
        if (!Directory.Exists(assetsPath + "/assets/bundledassets/videos/profesora/repeticion_acierto"))
            Directory.CreateDirectory(assetsPath + "/assets/bundledassets/videos/profesora/repeticion_acierto");
        if (!Directory.Exists(assetsPath + "/assets/bundledassets/videos/profesora/repeticion_error"))
            Directory.CreateDirectory(assetsPath + "/assets/bundledassets/videos/profesora/repeticion_error");
        if (!Directory.Exists(assetsPath + "/assets/bundledassets/videos/profesora/sesion"))
            Directory.CreateDirectory(assetsPath + "/assets/bundledassets/videos/profesora/sesion");
        if (!Directory.Exists(assetsPath + "/assets/bundledassets/videos/profesora/sesion/inicio"))
            Directory.CreateDirectory(assetsPath + "/assets/bundledassets/videos/profesora/sesion/inicio");
        if (!Directory.Exists(assetsPath + "/assets/bundledassets/videos/profesora/sesion/fin"))
            Directory.CreateDirectory(assetsPath + "/assets/bundledassets/videos/profesora/sesion/fin");
        if (!Directory.Exists(assetsPath + "/assets/bundledassets/videos/profesora/vocalizacion"))
            Directory.CreateDirectory(assetsPath + "/assets/bundledassets/videos/profesora/vocalizacion");

        // directorios videos2
        if (!Directory.Exists(assetsPath + "/assets/bundledassets/videos2"))
            Directory.CreateDirectory(assetsPath + "/assets/bundledassets/videos2");
        if (!Directory.Exists(assetsPath + "/assets/bundledassets/videos2/001 Avion"))
            Directory.CreateDirectory(assetsPath + "/assets/bundledassets/videos2/001 Avion");
        if (!Directory.Exists(assetsPath + "/assets/bundledassets/videos2/002 Pelota"))
            Directory.CreateDirectory(assetsPath + "/assets/bundledassets/videos2/002 Pelota");
        if (!Directory.Exists(assetsPath + "/assets/bundledassets/videos2/003 Perro"))
            Directory.CreateDirectory(assetsPath + "/assets/bundledassets/videos2/003 Perro");
        if (!Directory.Exists(assetsPath + "/assets/bundledassets/videos2/004 Gato"))
            Directory.CreateDirectory(assetsPath + "/assets/bundledassets/videos2/004 Gato");
        if (!Directory.Exists(assetsPath + "/assets/bundledassets/videos2/005 Dulce"))
            Directory.CreateDirectory(assetsPath + "/assets/bundledassets/videos2/005 Dulce");
        if (!Directory.Exists(assetsPath + "/assets/bundledassets/videos2/006 Zapato"))
            Directory.CreateDirectory(assetsPath + "/assets/bundledassets/videos2/006 Zapato");
        if (!Directory.Exists(assetsPath + "/assets/bundledassets/videos2/007 Jirafa"))
            Directory.CreateDirectory(assetsPath + "/assets/bundledassets/videos2/007 Jirafa");
        if (!Directory.Exists(assetsPath + "/assets/bundledassets/videos2/008 Nube"))
            Directory.CreateDirectory(assetsPath + "/assets/bundledassets/videos2/008 Nube");
        if (!Directory.Exists(assetsPath + "/assets/bundledassets/videos2/009 Libro"))
            Directory.CreateDirectory(assetsPath + "/assets/bundledassets/videos2/009 Libro");
        if (!Directory.Exists(assetsPath + "/assets/bundledassets/videos2/010 Paloma"))
            Directory.CreateDirectory(assetsPath + "/assets/bundledassets/videos2/010 Paloma");

        // directorios animaciones
        if (!Directory.Exists(assetsPath + "/assets/bundledassets/animaciones"))
            Directory.CreateDirectory(assetsPath + "/assets/bundledassets/animaciones");
        if (!Directory.Exists(assetsPath + "/assets/bundledassets/animaciones"))
            Directory.CreateDirectory(assetsPath + "/assets/bundledassets/animaciones");


    }

    private IEnumerator LoadAssetBundleCoroutine(string assetBundleName)
    {

        PlayAssetBundleRequest bundleRequest = PlayAssetDelivery.RetrieveAssetBundleAsync(assetBundleName);

        while (!bundleRequest.IsDone)
        {
            if (bundleRequest.Status == AssetDeliveryStatus.WaitingForWifi)
            {
                var userConfirmationOperation = PlayAssetDelivery.ShowCellularDataConfirmation();

                // Wait for confirmation dialog action.
                yield return userConfirmationOperation;

                if ((userConfirmationOperation.Error != AssetDeliveryErrorCode.NoError) ||
                   (userConfirmationOperation.GetResult() != ConfirmationDialogResult.Accepted))
                {
                    // The user did not accept the confirmation - handle as needed.
                }

                // Wait for Wi-Fi connection OR confirmation dialog acceptance before moving on.
                yield return new WaitUntil(() => bundleRequest.Status != AssetDeliveryStatus.WaitingForWifi);
            }

            // Use bundleRequest.Status to track the status of request.
            Messages.text = bundleRequest.Status.ToString();
            // Use bundleRequest.DownloadProgress to track download progress.
            if(bundleRequest.Status.ToString() == "Downloading")
            {
                Messages.text = "Downloading ... " + bundleRequest.DownloadProgress.ToString();
            }

            yield return null;
        }

        if (bundleRequest.Error != AssetDeliveryErrorCode.NoError)
        {
            // There was an error retrieving the bundle. For error codes NetworkError
            // and InsufficientStorage, you may prompt the user to check their
            // connection settings or check their storage space, respectively, then
            // try again.
            Messages.text = "bundleRequest.Error!!";
            yield return null;
        }

        // Request was successful. Retrieve AssetBundle from request.AssetBundle.
        packageBundle1 = bundleRequest.AssetBundle;
        Messages.text = "Package Bundle " + packageBundle1.ToString() + " Loaded!";

        // se copian todos los assets del AssetBundle recien descargado en assetsPath = Application.persistentDataPath
        //foreach (string assetName in assetsInBundles[assetBundleName])
        List<string> listOfAssets = new List<string>();
        foreach (string assetName in packageBundle1.GetAllAssetNames())
        {
            Messages.text = "Loading Asset " + assetName + "\n";
            TextAsset temp = packageBundle1.LoadAsset<TextAsset>(assetName);
            Messages.text = "Asset " + assetName + " Loaded!\n";
            Messages.text = "writing file to: " + assetsPath + "/" + assetName + "\n";
            File.WriteAllBytes(assetsPath + "/" + assetName , temp.bytes);
            Messages.text = "File: " + assetsPath + "/" + assetName + " Written!!\n";
            listOfAssets.Add(assetName);
            //yield return null;
        }

        // se guarda la lista de assets que contiene el bundle
        assetsInBundles[assetBundleName] = listOfAssets;
        writeAssetsInBundleFile(assetBundleName);

        // una vez copiado todos los assets a persistentdatapath se puede eliminar el AssetBundle de memoria y marcar el bundle como descargado
        packageBundle1.Unload(true);
        downloadedAssetBundles[assetBundleName] = true;
        writeDownloadedAssetBundlesFile();

        // por ultimo se vuelven a crear los items de scrollview con sus nuevos estados
        populateBundleScrollView();
        populateAssetScrollView(assetBundleName, true);

    } 

    public void downloadAssetBundle(string name)
    {
        StartCoroutine(LoadAssetBundleCoroutine(name));
    }

    public void deleteAssetBundle(string assetBundleName)
    {
        if (downloadedAssetBundles[assetBundleName])
        {
            // se elimina cada asset que fue copiado a persistentdatapath
            foreach (string asset in assetsInBundles[assetBundleName])
            {
                File.Delete(assetsPath + "/" + asset);
            }

            // se señala que el bundle ya no se encuentra descargado
            downloadedAssetBundles[assetBundleName] = false;
            writeDownloadedAssetBundlesFile();

            // se elimina el archivo con la lista de assets del bundle
            File.Delete(assetsPath + "/" + assetBundleName + ".txt");

            // se elimina la lista de assets que contiene el bundle
            assetsInBundles[assetBundleName] = new List<string>();

            // se vuelven a crear los items de scrollview con sus nuevos estados
            populateBundleScrollView();
            populateAssetScrollView("", false);
        }

    }

    // Asset states

    public void loadDownloadedAssetBundlesFileAndLoadAssetsInBundlesFile()
    {
        // se lee del archivo y se escribe el estado en el diccionario "downloadedAssetBundles"
        string[] fileContent = File.ReadAllLines(assetBundleHierarchyPath);
        downloadedAssetBundles.Clear();

        foreach (string line in fileContent)
        {
            string[] temp = line.Split(',');
            if (temp.Length == 2)
            {
                if(temp[1] == "t")
                {
                    downloadedAssetBundles.Add(temp[0], true);
                }
                else
                {
                    downloadedAssetBundles.Add(temp[0], false);
                }
            }
        }

        // se carga la lista de assets de cada bundle previamente descargado
        List<string> assetsList = new List<string>();
        Dictionary<string, bool>.KeyCollection keyColl = downloadedAssetBundles.Keys;
        foreach (string bundleName in keyColl)
        {
            if (downloadedAssetBundles[bundleName])
            {
                fileContent = File.ReadAllLines(assetsPath + "/" + bundleName + ".txt");
                assetsList.Clear();
                foreach(string line in fileContent)
                {
                    assetsList.Add(line);
                }
                assetsInBundles[bundleName] = assetsList;
            }
        }
    }
    
    public void writeDownloadedAssetBundlesFile()
    {
        // se escribe el estado del diccionario "downloadedAssetBundles" en el archivo assetBundleHierarchy.txt
        string fileContent = "";

        foreach (var item in downloadedAssetBundles)
        {
            if (item.Value == true)
            {
                fileContent += item.Key + ",t" + "\n";
            }
            else
            {
                fileContent += item.Key + ",f" + "\n";
            }
        }
        File.WriteAllText(assetBundleHierarchyPath, fileContent);
    }

    public void writeAssetsInBundleFile(string bundleName)
    {
        // se guarda en un archivo la lista de assets que contiene el bundle
        string fileContent = "";

        foreach (string asset in assetsInBundles[bundleName])
        {
            fileContent += asset + "\n";
        }
        File.WriteAllText(assetsPath + "/" + bundleName + ".txt", fileContent);
    }


    // Bundles ScrollView

    public void createBundleItem(string name, bool downloaded)
    {

        GameObject packButtonObject = Instantiate(PackButtonPrefab);
        packButtonObject.transform.SetParent(bundleContent, false);

        Button packButton = packButtonObject.GetComponent<Button>();
        packButton.onClick.AddListener(delegate { populateAssetScrollView(name, downloaded); });

        GameObject bundleNameObject = packButtonObject.transform.GetChild(0).gameObject;
        GameObject downloadPackButtonObject = packButtonObject.transform.GetChild (1).gameObject;
        GameObject deletePackButtonObject = packButtonObject.transform.GetChild (2).gameObject;

        Text bundleName = bundleNameObject.GetComponent<Text>();
        bundleName.text = "  " + name;

        Button downloadButton = downloadPackButtonObject.GetComponent<Button>();
        downloadButton.onClick.AddListener(delegate { downloadAssetBundle(name); });
        Button deleteButton = deletePackButtonObject.GetComponent<Button>();
        deleteButton.onClick.AddListener(delegate { deleteAssetBundle(name); });

        if (downloaded)
        {
            downloadButton.interactable = false;
            deleteButton.interactable = true;
        }
        else
        {
            downloadButton.interactable = true;
            deleteButton.interactable = false;
        }


    }

    public void populateBundleScrollView()
    {
        // se borran los items antiguos
        foreach (Transform child in bundleContent)
        {
            Destroy(child.gameObject);
        }

        Dictionary<string, bool>.KeyCollection keyColl = downloadedAssetBundles.Keys;
        foreach (string bundleName in keyColl)
        {
            createBundleItem(bundleName, downloadedAssetBundles[bundleName]);
        }
    }

    // Assets ScrollView

    public void createAssetItem(string assetName, string assetBundleName, bool downloaded)
    {
        GameObject videoButtonObject = Instantiate(VideoButtonPrefab);
        videoButtonObject.transform.SetParent(assetContent, false);

        GameObject playAssetNameObject = videoButtonObject.transform.GetChild(0).gameObject;
        Text assetNameText = playAssetNameObject.GetComponent<Text>();
        string[] temp = assetName.Split('/');
        assetNameText.text = "  " + temp[temp.Length - 1];

        GameObject playAssetButtonObject = videoButtonObject.transform.GetChild(1).gameObject;
        Button playButton = playAssetButtonObject.GetComponent<Button>();
        playButton.onClick.AddListener(delegate { playVideoAsset(assetName, assetBundleName); });

        if (downloaded)
        {
            playButton.interactable = true;
        }
        else
        {
            playButton.interactable = false;
        }



    }

    public void populateAssetScrollView(string bundleName, bool downloaded)
    {
        // se borran los items antiguos
        foreach (Transform child in assetContent)
        {
            Destroy(child.gameObject);
        }

        if (string.Compare(bundleName, "") != 0 && assetsInBundles[bundleName].Count != 0)
        {
            // se agregan los items del bundle seleccionado
            List<string> assetsList = assetsInBundles[bundleName];
            foreach (string s in assetsList)
            {
                createAssetItem(s, bundleName, downloaded);
            }
        }

    }

    public void quitApp()
    {
        Application.Quit();
    }


    /*
    ////////////////////////////////////////////////////////////////////////////////////////////////
    ////////////////// VERSION DE CARGA CON ASSETBUNDLE DESDE STREAMINGASSETSPATH //////////////////
    ////////////////////////////////////////////////////////////////////////////////////////////////

    MediaPlayer1    = GameObject.Find("MediaPlayer1").GetComponent<MediaPlayer>();
    MediaPlayer2    = GameObject.Find("MediaPlayer2").GetComponent<MediaPlayer>();
    Messages        = GameObject.Find("messages").GetComponent<Text>();

    packageBundle1 = AssetBundle.LoadFromFile(Path.Combine(Application.streamingAssetsPath, "videopackage1"));
    packageBundle2 = AssetBundle.LoadFromFile(Path.Combine(Application.streamingAssetsPath, "videopackage2"));

    if(packageBundle1 == null || packageBundle2 == null)
    {
        Debug.LogError("Error al cargar AssetBundles!");
        return;
    }

    TextAsset video1 = packageBundle1.LoadAsset<TextAsset>("gato1.mp4.bytes");
    TextAsset video2 = packageBundle2.LoadAsset<TextAsset>("gato2.mp4.bytes");

    File.WriteAllBytes(Application.persistentDataPath + "/gato1.mp4", video1.bytes);
    File.WriteAllBytes(Application.persistentDataPath + "/gato2.mp4", video2.bytes);

    MediaPlayer1.OpenMedia(MediaPathType.RelativeToPersistentDataFolder, "gato1.mp4");
    MediaPlayer2.OpenMedia(MediaPathType.RelativeToPersistentDataFolder, "gato2.mp4");

    MediaPlayer1.Play();
    MediaPlayer2.Play();

    //Debug.Log();
    //Debug.Log();

    */


    /*

    ////////////////////////////////////////////////////////////////////////////////////////////////
    ////////////////// VERSION DE CARGA CON ASSETBUNDLE DESDE PLAYASSETDELIVERY ! //////////////////
    ////////////////////////////////////////////////////////////////////////////////////////////////

    private IEnumerator LoadAssetBundleCoroutine(string assetBundleName)
    {

        PlayAssetBundleRequest bundleRequest = PlayAssetDelivery.RetrieveAssetBundleAsync(assetBundleName);

        while (!bundleRequest.IsDone)
        {
            if (bundleRequest.Status == AssetDeliveryStatus.WaitingForWifi)
            {
                var userConfirmationOperation = PlayAssetDelivery.ShowCellularDataConfirmation();

                // Wait for confirmation dialog action.
                yield return userConfirmationOperation;

                if ((userConfirmationOperation.Error != AssetDeliveryErrorCode.NoError) ||
                   (userConfirmationOperation.GetResult() != ConfirmationDialogResult.Accepted))
                {
                    // The user did not accept the confirmation - handle as needed.
                }

                // Wait for Wi-Fi connection OR confirmation dialog acceptance before moving on.
                yield return new WaitUntil(() => bundleRequest.Status != AssetDeliveryStatus.WaitingForWifi);
            }

            // Use bundleRequest.DownloadProgress to track download progress.
            // Use bundleRequest.Status to track the status of request.
            Messages.text = bundleRequest.Status.ToString();

            yield return null;
        }

        if (bundleRequest.Error != AssetDeliveryErrorCode.NoError)
        {
            // There was an error retrieving the bundle. For error codes NetworkError
            // and InsufficientStorage, you may prompt the user to check their
            // connection settings or check their storage space, respectively, then
            // try again.
            Messages.text = "bundleRequest.Error!!";
            yield return null;
        }

        // Request was successful. Retrieve AssetBundle from request.AssetBundle.
        packageBundle1 = bundleRequest.AssetBundle;
        Messages.text = packageBundle1.ToString();

        TextAsset video1 = packageBundle1.LoadAsset<TextAsset>("gato1.mp4.bytes");

        File.WriteAllBytes(Application.persistentDataPath + "/gato1.mp4", video1.bytes);

        MediaPlayer1.OpenMedia(MediaPathType.RelativeToPersistentDataFolder, "gato1.mp4");
        MediaPlayer1.Play();

    }

    */

}


/*
https://docs.huihoo.com/unity/5.3/Documentation/en/Manual/AssetBundlesIntro.html
https://answers.unity.com/questions/1714181/av-pro-how-to-play-video-from-asset-bundle.html
https://learn.unity.com/tutorial/introduction-to-asset-bundles#6028be40edbc2a112d4f4fe5
https://stackoverflow.com/questions/45580717/playing-large-video-files-in-gear-vr-in-unity-project?noredirect=1#comment78162170_45580717
https://forums.oculusvr.com/t5/Unity-Development/Expansion-File-Bundled-Scene-Not-Loading-Video-Go/td-p/667827
https://answers.unity.com/questions/1521925/how-do-you-add-text-to-a-scroll-view-programatical.html

*/