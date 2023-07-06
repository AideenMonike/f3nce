using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class onTriggerEnter : UnityEvent <Collider> {}

[System.Serializable]
public class onTriggerStay : UnityEvent <Collider> {}

[System.Serializable]
public class onTriggerExit : UnityEvent <Collider> {}