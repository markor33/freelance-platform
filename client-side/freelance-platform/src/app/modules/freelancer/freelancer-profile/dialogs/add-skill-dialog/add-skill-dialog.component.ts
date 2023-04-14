import { Component } from '@angular/core';
import { ProfessionService } from 'src/app/modules/shared/services/profession.service';
import { FreelancerService } from '../../../services/freelancer.service';
import { Skill } from 'src/app/modules/shared/models/profession.mode';
import { FormControl } from '@angular/forms';
import { AddSkillCommand } from '../../../models/add-skill-command.model';
import { MatDialogRef } from '@angular/material/dialog';
import { SnackBarsService } from 'src/app/modules/shared/services/snack-bars.service';

@Component({
  selector: 'app-add-skill-dialog',
  templateUrl: './add-skill-dialog.component.html',
  styleUrls: ['./add-skill-dialog.component.scss']
})
export class AddSkillDialogComponent {

  selectedSkills: Skill[] = [];
  allSkills: Skill[] = [];
  skillsControl = new FormControl<Skill[]>([]);

  constructor(
    private dialogRef: MatDialogRef<AddSkillDialogComponent>,
    private professionService: ProfessionService,
    private freelancerService: FreelancerService,
    private snackBars: SnackBarsService) { }

  ngOnInit() {
    this.freelancerService.freelancerObserver.subscribe((freelancer) => {
      var professionId = freelancer?.profession.id;
      this.professionService.getSkills(professionId as string).subscribe({
        next: (skills) => this.allSkills = skills
      });
    })
  }

  remove(skill: Skill) {
    const skills = this.skillsControl.value;
    const index = skills?.indexOf(skill) as number;

    skills?.splice(index, 1);
    this.skillsControl.setValue(skills);
  }

  addSkills() {
    this.freelancerService.addSkills(this.skillsControl.value as Skill[]).subscribe({
      complete: this.skillsSuccessfullyAdded.bind(this)
    });
  }

  skillsSuccessfullyAdded() {
    this.snackBars.primary('Skills successfully added');
    this.dialogRef.close();
  }

}
