# ai-quest-generator
A program that considers world conditions and then uses a LLM to generate quests.

This is the main design document for general roadmaps as to how I want to handle this. I previously wrote the following in a txt file.


## Overall Plan
I want to make a system that understands the current world and then establishes a few things. 

It must use the references from the industry skills that the gov tracks for basic world stuff, but I would I also want it to include magic systems 

1. generates quests based on world conditions
2. generates individual characters with unique names and jobs.
3. each job will have an associated level of skills. 

Once a char is created, I want them to be referenced in a db with the relational system saving there basic info (skills, name, job, action history)


Goal is a strategy game like world that plays by itself.


Its clear that this will need several individual microservices to handle it. 
- At least one needs to be focused on establishing the main world.
- character storage will need to be another
- npc generator 
- conversation generator - orchestrator between the npc, world, and quest handlers (lot of demanding work here)
- the skill tracker would be good but we also want to make sure there is a balance of created/generated and they don't overlap (gotta use a baseline here)
- quest generator could be its own as well
- job name generator would be dependent on the skill tracker since it
- faction organizer - this would be associated with the city or other association of an npc or any character.
- resource manager

This does not need to be done all at once. it only needs to be done in parts. but once the parts come whole it will create a fun system.
