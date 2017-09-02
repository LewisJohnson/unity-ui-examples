import os
import string
from shutil import copyfile

folders = ["Animations", "Images", "Prefabs", "Scenes", "Scripts"]


def get_type():
    print("1 - Screen Space")
    print("2 - World Space")
    example_type = int(input())

    if example_type == 1:
        return "ScreenSpace"
    elif example_type == 2:
        return "WorldSpace"
    else:
        get_type()


def get_project_name():
    print("Enter a new example name:")
    example_name = input()

    if os.path.exists(os.path.join(os.path.curdir, 'Assets', 'ScreenSpace', example_name))\
            or os.path.exists(os.path.join(os.path.curdir, 'Assets', 'WorldSpace', example_name)):
        print("Example \"" + example_name + "\" already exists.")
        get_project_name()
    else:
        # We have to follow guidelines :)
        example_name = string.capwords(example_name)
        example_name = example_name.replace(' ', '')
        return example_name


if __name__ == "__main__":
    proj_type = get_type()
    proj_name = get_project_name()
    scene_name = proj_name + ".unity"

    # Create the new folders
    for folder in folders:
        os.makedirs(os.path.join(os.path.curdir, 'Assets', proj_type, proj_name, folder))

    # Copy scene from skeleton
    copyfile(os.path.join(os.path.curdir, 'Skeleton', 'Scene.unity'),
             os.path.join(os.path.curdir, 'Assets', proj_type, proj_name, 'Scenes', scene_name))

    # Copy readme from skeleton
    copyfile(os.path.join(os.path.curdir, 'Skeleton', 'README.md'),
             os.path.join(os.path.curdir, 'Assets', proj_type, proj_name, 'README.md'))

    # Copy image from skeleton
    copyfile(os.path.join(os.path.curdir, 'Skeleton', 'readme-image.png'),
             os.path.join(os.path.curdir, 'Assets', proj_type, proj_name, 'readme-image.png'))


