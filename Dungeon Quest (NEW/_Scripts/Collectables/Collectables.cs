﻿using System;
using System.Collections;
using System.Collections.Generic; 
using UnityEngine;

public class Collectables : MonoBehaviour
{    
    [Header("Settings")]
    [SerializeField] private bool canDestroyItem = true;
    
    protected Character character;
    protected GameObject objectCollided;
    protected SpriteRenderer spriteRenderer;
    new Collider2D collider2D;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        collider2D = GetComponent<Collider2D>();
	} 

    private void OnTriggerEnter2D(Collider2D other)
    {
        objectCollided = other.gameObject;
        if (IsPickable())
        {
            Pick();
            PlayEffects();

            if (canDestroyItem)
            {
                Destroy(gameObject);
            }
            else
            {
                spriteRenderer.enabled = false;
                collider2D.enabled = false;
            }
        }
	} 

    protected virtual bool IsPickable()
    {
        character = objectCollided.GetComponent<Character>();
        if (character == null)
        {
            return false;
        }

        return character.CharacterType == Character.CharacterTypes.Player;
	}

    protected virtual void Pick()
    {
        // ---
    }

    protected virtual void PlayEffects()
    {
        // -------        
    }
}