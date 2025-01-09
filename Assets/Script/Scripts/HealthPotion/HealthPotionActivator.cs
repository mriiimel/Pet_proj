using System.Collections;
using UnityEngine;


public class HealthPotionActivator : MonoBehaviour
{
    private ScriptableObjectService _scriptableObjectService;

    private GameObject _bigHalthPotion;
    private GameObject _smallHealthPotion;
    

    public void Construct(ScriptableObjectService scriptableObjectService,GameObject bigHealthPotion,
        GameObject smallHealthPotion)
    {
        _scriptableObjectService = scriptableObjectService;
        _bigHalthPotion = bigHealthPotion;
        _smallHealthPotion = smallHealthPotion;

    }
    private void Start()
    {
        
        StartCoroutine(ActivateBigHealethPotion());
        StartCoroutine(ActivateSmallHealethPotion());
    }

    private IEnumerator ActivateBigHealethPotion()
    {
        while (true)
        {
            if (!_smallHealthPotion.activeInHierarchy) 
            {
                
                yield return new WaitForSeconds(_scriptableObjectService.PotionConfig.GetPotion(_bigHalthPotion.GetComponent<HealthPotion>().PotionType).CoolDown);
                _bigHalthPotion.SetActive(true);
                
                yield return new WaitUntil(() => !_bigHalthPotion.activeInHierarchy);
            }
            yield return null;
        }
    }
    private IEnumerator ActivateSmallHealethPotion()
    {
        while (true)
        {
            if (!_bigHalthPotion.activeInHierarchy)
            {
                yield return new WaitForSeconds(_scriptableObjectService.PotionConfig.GetPotion(_smallHealthPotion.GetComponent<HealthPotion>().PotionType).CoolDown);
                _smallHealthPotion.SetActive(true);

                yield return new WaitUntil(() => !_smallHealthPotion.activeInHierarchy);
            }
            yield return null;
        }
    }

    private void OnDisable()
    {
        StopCoroutine(ActivateSmallHealethPotion());
        StopCoroutine(ActivateBigHealethPotion());
    }
}