import { Component } from '@angular/core';
import { ProfessionService } from 'src/app/modules/shared/services/profession.service';
import { FreelancerService } from '../../../services/freelancer.service';
import { Skill } from 'src/app/modules/shared/models/profession.mode';
import { FormControl } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { SnackBarsService } from 'src/app/modules/shared/services/snack-bars.service';
import { AddSkillCommand } from '../../../models/add-skill-command.model';

@Component({
  selector: 'app-add-skill-dialog',
  templateUrl: './add-skill-dialog.component.html',
  styleUrls: ['./add-skill-dialog.component.scss']
})
export class AddSkillDialogComponent {

  addSkillCommand = new AddSkillCommand();

  allSkills: Skill[] = [];
  skillsControl = new FormControl<Skill[]>([]);

  constructor(
    private dialogRef: MatDialogRef<AddSkillDialogComponent>,
    private professionService: ProfessionService,
    private freelancerService: FreelancerService,
    private snackBars: SnackBarsService) { }

  ngOnInit() {
    const professionId = this.freelancerService.currentFreelancer.profession.id;
    this.professionService.getSkills(professionId as string).subscribe({
      next: (skills) => this.allSkills = skills
    });
  }

  remove(skill: Skill) {
    const skills = this.skillsControl.value;
    const index = skills?.indexOf(skill) as number;

    skills?.splice(index, 1);
    this.skillsControl.setValue(skills);
  }

  addSkills() {
    this.addSkillCommand.skills = (this.skillsControl.value as Skill[]).map((skill) => skill.id);
    this.freelancerService.addSkills(this.addSkillCommand).subscribe({
      complete: this.skillsSuccessfullyAdded.bind(this)
    });
  }

  skillsSuccessfullyAdded() {
    this.snackBars.primary('Skills successfully added');
    this.dialogRef.close();
  }

}
