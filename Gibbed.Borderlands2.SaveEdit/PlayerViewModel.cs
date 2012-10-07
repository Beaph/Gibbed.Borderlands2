﻿/* Copyright (c) 2012 Rick (rick 'at' gibbed 'dot' us)
 * 
 * This software is provided 'as-is', without any express or implied
 * warranty. In no event will the authors be held liable for any damages
 * arising from the use of this software.
 * 
 * Permission is granted to anyone to use this software for any purpose,
 * including commercial applications, and to alter it and redistribute it
 * freely, subject to the following restrictions:
 * 
 * 1. The origin of this software must not be misrepresented; you must not
 *    claim that you wrote the original software. If you use this software
 *    in a product, an acknowledgment in the product documentation would
 *    be appreciated but is not required.
 * 
 * 2. Altered source versions must be plainly marked as such, and must not
 *    be misrepresented as being the original software.
 * 
 * 3. This notice may not be removed or altered from any source
 *    distribution.
 */

using System.Collections.Generic;
using System.ComponentModel.Composition;
using Caliburn.Micro;

namespace Gibbed.Borderlands2.SaveEdit
{
    [Export(typeof(PlayerViewModel))]
    internal class PlayerViewModel : PropertyChangedBase, IHandle<SaveUnpackMessage>
    {
        #region Fields
        private FileFormats.SaveFile _SaveFile;
        #endregion

        #region Properties
        public FileFormats.SaveFile SaveFile
        {
            get { return this._SaveFile; }
            private set
            {
                if (this._SaveFile != value)
                {
                    this._SaveFile = value;
                    this.NotifyOfPropertyChange(() => this.SaveFile);
                }
            }
        }

        public List<PlayerClassDefinition> ClassDefinitions { get; private set; }
        #endregion

        [ImportingConstructor]
        public PlayerViewModel(IEventAggregator events)
        {
            this.ClassDefinitions = new List<PlayerClassDefinition>();
            this.ClassDefinitions.Add(new PlayerClassDefinition("Axton (Commando)",
                                                                "GD_Soldier.Character.CharClass_Soldier"));
            this.ClassDefinitions.Add(new PlayerClassDefinition("Zer0 (Assassin)",
                                                                "GD_Assassin.Character.CharClass_Assassin"));
            this.ClassDefinitions.Add(new PlayerClassDefinition("Maya (Siren)", "GD_Siren.Character.CharClass_Siren"));
            this.ClassDefinitions.Add(new PlayerClassDefinition("Salvador (Gunzerker)",
                                                                "GD_Mercenary.Character.CharClass_Mercenary"));
            //this.ClassDefinitions.Add(new PlayerClassDefinition("Gaige (Mechromancer)", "UNKNOWN"));

            events.Subscribe(this);
        }

        public void Handle(SaveUnpackMessage message)
        {
            this.SaveFile = message.SaveFile;
        }
    }
}