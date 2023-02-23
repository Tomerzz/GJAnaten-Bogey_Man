using TMPro;
using UnityEngine;

public abstract class IPlayer : MonoBehaviour
{
    /// <summary>
    /// Si on est attraper ou non
    /// </summary>
    public abstract bool GetCought { get; set; }

    /// <summary>
    /// Si le monstre est cacher ou non
    /// </summary>
    public abstract bool IsHiding { get; set; }

    /// <summary>
    /// Si le monstre peut se cacher
    /// </summary>
    public abstract bool CanHide { get; set; }

    /// <summary>
    /// Le son emis par le monstre
    /// </summary>
    public abstract float CurrentSound { get; set; }

    /// <summary>
    /// Le Monstre peut faire peur à l'enfant
    /// </summary>
    public abstract bool CanFear { get; set; }

    /// <summary>
    /// Panel d'interaction du monstre
    /// </summary>
    public abstract GameObject PanelInteract { get; set; }

    /// <summary>
    /// Texte du panel d'interaction
    /// </summary>
    public abstract TextMeshProUGUI TxtInteract { get; set; }

    /// <summary>
    /// Faire faire du bruit au monstre
    /// </summary>
    /// <param name="sound">
    /// de 0 à 100
    /// </param>
    public abstract void DoingSound(float sound);
}
