﻿using System.IO;
using UnityEngine;
using XUnity.AutoTranslator.Plugin.Core.Textures;

namespace XUnity.AutoTranslator.Plugin.Core.Managed.Textures
{
   class TgaImageLoader : ITextureLoader
   {
      public void Load( object texture, byte[] data )
      {
         if( texture == null && data == null ) return;

         Texture2D texture2D = (Texture2D)texture;
         var format = texture2D.format;

         using( var stream = new MemoryStream( data ) )
         using( var binaryReader = new BinaryReader( stream ) )
         {
            binaryReader.BaseStream.Seek( 12L, SeekOrigin.Begin );
            short num1 = binaryReader.ReadInt16();
            short num2 = binaryReader.ReadInt16();
            int num3 = (int)binaryReader.ReadByte();
            binaryReader.BaseStream.Seek( 1L, SeekOrigin.Current );
            Color32[] colors = new Color32[ (int)num1 * (int)num2 ];
            if( format == TextureFormat.RGB24 )
            {
               if( num3 == 32 )
               {
                  for( int index = 0; index < (int)num1 * (int)num2; ++index )
                  {
                     byte b = binaryReader.ReadByte();
                     byte g = binaryReader.ReadByte();
                     byte r = binaryReader.ReadByte();
                     binaryReader.ReadByte();
                     colors[ index ] = new Color( r, g, b, 1f );
                  }
               }
               else
               {
                  for( int index = 0; index < (int)num1 * (int)num2; ++index )
                  {
                     byte b = binaryReader.ReadByte();
                     byte g = binaryReader.ReadByte();
                     byte r = binaryReader.ReadByte();
                     colors[ index ] = new Color( r, g, b, 1f );
                  }
               }
            }
            else
            {
               if( num3 == 32 )
               {
                  for( int index = 0; index < (int)num1 * (int)num2; ++index )
                  {
                     byte b = binaryReader.ReadByte();
                     byte g = binaryReader.ReadByte();
                     byte r = binaryReader.ReadByte();
                     byte a = binaryReader.ReadByte();
                     colors[ index ] = new Color( r, g, b, a );
                  }
               }
               else
               {
                  for( int index = 0; index < (int)num1 * (int)num2; ++index )
                  {
                     byte b = binaryReader.ReadByte();
                     byte g = binaryReader.ReadByte();
                     byte r = binaryReader.ReadByte();
                     colors[ index ] = new Color( r, g, b, 1f );
                  }
               }
            }
            texture2D.SetPixels32( colors );
            texture2D.Apply();
         }
      }

      public bool Verify()
      {
         Load( null, null ); // Verify by making sure that the method can be JITed

         return true;
      }
   }
}