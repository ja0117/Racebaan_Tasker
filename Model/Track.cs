using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class Track
    {
        public string Name { get; set; }
        public LinkedList<Section> Sections { get; set; }

        public Track(string name, SectionTypes[] sections)
        {
            this.Name = name;
            this.Sections = SectionTypesToLinkedList(sections);
        }

        // Level 4 task 1
        public LinkedList<Section> SectionTypesToLinkedList(SectionTypes[] sectionTypes)
        {
            LinkedList<Section> section = new LinkedList<Section>();
            foreach (SectionTypes sectionType in sectionTypes)
            {
                Section s = new Section(sectionType);
                section.AddLast(s);
            }
            return section;
        }
    }
}
